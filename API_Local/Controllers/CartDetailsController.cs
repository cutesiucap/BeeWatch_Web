﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API_Local.Models;

namespace API_Local.Controllers
{
    public class CartDetailsController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/CartDetails
        public IQueryable<CartDetails> GetCartDetails()
        {
            return db.CartDetails;
        }

        [Route("api/GetbyIdAccount/{id}")]
        [HttpGet]
        // GET: api/view_CartDetails
        public IQueryable<view_CartDetailHome> GetbyIdAccount(int id)
        {
            IQueryable<view_CartDetailHome> view_CartDetailHomes = db.view_CartDetailHome.Where(x => x.id_Cart == id);
            foreach(view_CartDetailHome view_CartDetailHome in view_CartDetailHomes)
            {
                Image image = db.Image.Where(x => x.id_Watches == view_CartDetailHome.id_Watch).FirstOrDefault();
                if (image != null)
                {
                    view_CartDetailHome.Url_Image = image.Url_Image;
                }
                
            }
            return view_CartDetailHomes;
        }

        // GET: api/CartDetails/5
        [ResponseType(typeof(CartDetails))]
        public IHttpActionResult GetCartDetails(int id)
        {
            CartDetails cartDetails = db.CartDetails.Find(id);
            if (cartDetails == null)
            {
                return NotFound();
            }

            return Ok(cartDetails);
        }

        [HttpPost]
        public IHttpActionResult AddCartDetail(CartDetails cartDetails)
        {
            if (db.CartDetails.Where(x => x.id_Cart == cartDetails.id_Cart && x.id_Watch == cartDetails.id_Watch).FirstOrDefault() != null)
            {
                db.Entry(cartDetails).State = EntityState.Modified;
            }
            else
            {
                db.CartDetails.Add(cartDetails);
            }
            db.SaveChanges();
            return Ok();
        }

        // PUT: api/CartDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCartDetails(int id, CartDetails cartDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cartDetails.id_Watch)
            {
                return BadRequest();
            }

            db.Entry(cartDetails).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/CartDetails/Add")]
        [HttpPost]
        // POST: api/CartDetails
        [ResponseType(typeof(CartDetails))]
        public IHttpActionResult Add(CartDetails cartDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CartDetails cartDetails1 = db.CartDetails.Where(x => x.id_Watch == cartDetails.id_Watch && x.id_Cart == cartDetails.id_Cart).FirstOrDefault();
            if(cartDetails1 != null)
            {
                cartDetails1.Count += cartDetails.Count;
                db.Entry(cartDetails1).State = EntityState.Modified;
                db.SaveChanges();
                return BadRequest();
            }
            else
            {
                db.CartDetails.Add(cartDetails);
                db.SaveChanges();
                return Ok();
            }
        }

        [Route("api/CartDetails/Change")]
        [HttpPost]
        // POST: api/CartDetails
        [ResponseType(typeof(CartDetails))]
        public IHttpActionResult Change(CartDetails cartDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CartDetails cartDetails1 = db.CartDetails.Where(x => x.id_Watch == cartDetails.id_Watch && x.id_Cart == cartDetails.id_Cart).FirstOrDefault();
            if (cartDetails1 != null)
            {
                //Chỉnh Check
                if (cartDetails.IsCheck != null)
                {
                    cartDetails1.IsCheck = cartDetails.IsCheck;
                    db.SaveChanges();
                    return Ok(cartDetails1);
                }
                else
                {
                    //CHỉnh Số lượng
                    cartDetails1.Count = cartDetails.Count;
                    if (cartDetails1.Count == 0)
                    {
                        db.CartDetails.Remove(cartDetails1);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(cartDetails1).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Ok(cartDetails1);
                }
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/CartDetails/5
        [ResponseType(typeof(CartDetails))]
        public IHttpActionResult DeleteCartDetails(int id)
        {
            CartDetails cartDetails = db.CartDetails.Find(id);
            if (cartDetails == null)
            {
                return NotFound();
            }

            db.CartDetails.Remove(cartDetails);
            db.SaveChanges();

            return Ok(cartDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartDetailsExists(int id)
        {
            return db.CartDetails.Count(e => e.id_Watch == id) > 0;
        }

        //Cộng số lượng của Item trong Cart Detail lên 1 (thao tác '+' trên Giao diện Giỏ Hàng)
        // Input:   id_Cart         [FromBody]
        //          id_CartDetail   [FromBody]
        // Output:  Ok(1)           Update Bản ghi
        //          Ok(2)           Thêm Bản ghi
        //          BadRequest      Lỗi SQL
        //          NotFound        Không tìm thấy
        [Route("api/CartDetails/AddCountItem/{id_Cart:int}/{id_CartDetail:int}")]
        [HttpPost]
        [ResponseType(typeof(int))]
        public IHttpActionResult AddCountItem([FromUri] int id_Cart, [FromUri] int id_CartDetail)
        {
            if (id_Cart <= 0 || id_CartDetail <= 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idCartParam = new SqlParameter("idCart", SqlDbType.Int);
            idCartParam.Value = id_Cart;

            var idWatchParam = new SqlParameter("idWatch", SqlDbType.Int);
            idWatchParam.Value = id_CartDetail;

            int result;

            try
            {
                result = db.Database.ExecuteSqlCommand("EXEC [dbo].[sp_InsertCartDetails]  @idCart, @idWatch",
                                                                  idCartParam, id_CartDetail);
            }
            catch (Exception e)
            {
                return BadRequest(message: e.Message);
            }

            switch (result)
            {
                case 1:
                    return Ok(1);
                case 2:
                    return Ok(2);
                case 0:
                    return NotFound();
            }

            return BadRequest();
        }

        //Trừ số lượng của Item trong Cart Detail xuống 1 (thao tác '-' trên Giao diện Giỏ Hàng)
        // Input:   id_Cart         [FromBody]
        //          id_CartDetail   [FromBody]
        // Output:  Ok(1)           Trừ Bản ghi
        //          Ok(2)           Xóa Bản ghi
        //          BadRequest      Lỗi SQL
        //          NotFound        Không tìm thấy
        [Route("api/CartDetails/SubCountItem/{id_Cart:int}/{id_CartDetail:int}")]
        [HttpPost]
        [ResponseType(typeof(int))]
        public IHttpActionResult SubCountItem([FromUri] int id_Cart, [FromUri] int id_CartDetail)
        {
            if (id_Cart <= 0 || id_CartDetail <= 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idCartParam = new SqlParameter("idCart", SqlDbType.Int);
            idCartParam.Value = id_Cart;

            var idWatchParam = new SqlParameter("idWatch", SqlDbType.Int);
            idWatchParam.Value = id_CartDetail;

            int result;

            try
            {
                result = db.Database.ExecuteSqlCommand("EXEC [dbo].[sp_DropCartDetails]  @idCart, @idWatch",
                                                                  idCartParam, id_CartDetail);
            }
            catch (Exception e)
            {
                return BadRequest(message: e.Message);
            }

            switch (result)
            {
                case 1:
                    return Ok(1);
                case 2:
                    return Ok(2);
                case 0:
                    return NotFound();
            }

            return BadRequest();
        }

        //Xóa Item trong Cart Detail (thao tác 'xóa' trên Giao diện Giỏ Hàng)
        // Input:   id_Cart         [FromUri]
        //          id_CartDetail   [FromBody]
        // Output:  Ok              Xóa Thành công
        //          BadRequest      Lỗi SQL
        //          NotFound        Không tìm thấy
        [Route("api/CartDetails/DeleteItem/{id_Cart:int}/{id_CartDetail:int}")]
        [HttpPost]
        [ResponseType(typeof(int))]
        public IHttpActionResult DeleteItem([FromUri] int id_Cart, [FromUri] int id_CartDetail)
        {
            if (id_Cart <= 0 || id_CartDetail <= 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idCartParam = new SqlParameter("idCart", SqlDbType.Int);
            idCartParam.Value = id_Cart;

            var idWatchParam = new SqlParameter("idWatch", SqlDbType.Int);
            idWatchParam.Value = id_CartDetail;

            int result;

            try
            {
                result = db.Database.ExecuteSqlCommand("EXEC [dbo].[sp_DeleteCartDetails]  @idCart, @idWatch",
                                                                  idCartParam, id_CartDetail);
            }
            catch (Exception e)
            {
                return BadRequest(message: e.Message);
            }

            switch (result)
            {
                case 1:
                    return Ok();
                case 0:
                    return NotFound();
            }

            return BadRequest();
        }

        //thay đổi số lượng của Item trong Cart Detail (thao tác chỉnh sửa số lượng trên Giao diện Giỏ Hàng)
        // Input:   id_Cart         [FromUri]
        //          id_CartDetail   [FromBody]
        //          count           [FromBody]
        // Output:  Ok              Sửa số lượng thành công
        //          BadRequest      Lỗi SQL
        //          NotFound        Không tìm thấy
        [Route("api/CartDetails/UpdateCountItem/{id_Cart:int}/{id_CartDetail:int}/{count:int}")]
        [HttpPost]
        [ResponseType(typeof(int))]
        public IHttpActionResult UpdateCountItem([FromUri] int id_Cart, [FromUri] int id_CartDetail, [FromUri] int count)
        {
            if (id_Cart <= 0 || id_CartDetail <= 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idCartParam = new SqlParameter("idCart", SqlDbType.Int);
            idCartParam.Value = id_Cart;

            var idWatchParam = new SqlParameter("idWatch", SqlDbType.Int);
            idWatchParam.Value = id_CartDetail;

            var countParam = new SqlParameter("count", SqlDbType.Int);
            countParam.Value = count;

            int result;

            try
            {
                result = db.Database.ExecuteSqlCommand("EXEC [dbo].[sp_UpdateCountCartDetails]  @idCart, @idWatch, @count",
                                                                  idCartParam, id_CartDetail, countParam);
            }
            catch (Exception e)
            {
                return BadRequest(message: e.Message);
            }

            switch (result)
            {
                case 1:
                    return Ok();
                case 0:
                    return NotFound();
            }

            return BadRequest();
        }
    }
}