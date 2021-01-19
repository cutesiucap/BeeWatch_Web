using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API_Local.Models;

namespace API_Local.Controllers
{
    public class OrdersController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Orders
        public IQueryable<view_Orders> GetOrders()
        {
            return db.view_Orders;
        }
        [Route("api/count_order")]
        [HttpGet]

        [Route("api/UpStatus/{id}")]
        [HttpPost]
        public IHttpActionResult UpStatus(int id)
        {
            Orders orders = db.Orders.Find(id);
            if(orders.Status < 3)
            {
                orders.Status = orders.Status + 1;
                foreach (var item in db.OrderDetails.Where(x => x.id_Order == id))
                {
                    if (item.Status < orders.Status)
                    {
                        item.Status = orders.Status;
                        db.Entry(item).State = EntityState.Modified;
                    }
                }
                db.SaveChanges();
            }
            return Ok();            
        }

        [Route("api/CancelStatus/{id}")]
        [HttpPost]
        public IHttpActionResult CancelStatus(int id)
        {
            Orders orders = db.Orders.Find(id);
            orders.Status = 4;
            foreach (var item in db.OrderDetails.Where(x => x.id_Order == id))
            {
                if (item.Status < orders.Status)
                {
                    item.Status = orders.Status;
                    db.Entry(item).State = EntityState.Modified;
                }
            }
            db.SaveChanges();
            return Ok();
        }

        // GET: api/Orders/5
        [ResponseType(typeof(view_Orders))]
        public IHttpActionResult GetOrders(int id)
        {
            view_Orders orders = db.view_Orders.Where(x => x.id == id).FirstOrDefault();
            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrders(int id, Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orders.id)
            {
                return BadRequest();
            }

            db.Entry(orders).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
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

        // POST: api/Orders
        [ResponseType(typeof(Orders))]
        public IHttpActionResult PostOrders(Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(orders);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orders.id }, orders);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Orders))]
        public IHttpActionResult DeleteOrders(int id)
        {
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return NotFound();
            }

            db.Orders.Remove(orders);
            db.SaveChanges();

            return Ok(orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdersExists(int id)
        {
            return db.Orders.Count(e => e.id == id) > 0;
        }

        [Route("api/Orders/OrderofCustomer/{id:int}")]
        [HttpGet]
        [ResponseType(typeof(view_Orders))]
        public IHttpActionResult OrderofCustomer(int id)
        {
            view_Orders orders = db.view_Orders.Where(x => x.id_Accounts == id).FirstOrDefault();
            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [Route("api/Orders/OrdersofShop/{id:int}")]
        public IQueryable<view_Orders> OrdersofShop(int id)
        {
            return db.view_Orders.Where(x => x.id_Shop == id);
        }

        [Route("api/Orders/ShowOrder/{id:int}")]
        [HttpGet]
        public Models.Accounts ShowOrder(int id)
        {
            Accounts accounts = db.Accounts.Find(id);
            Accounts newaccount = new Accounts()
            {
                id = accounts.id,
                Fullname = accounts.Fullname,
                Email = accounts.Email,
                Url_Image_Avatar = accounts.Url_Image_Avatar,
                Point = accounts.Point,
            };

            foreach (var item in accounts.Phone)
            {
                item.Accounts = null;
                newaccount.Phone.Add(item);
            }

            foreach (var item in accounts.Address)
            {
                
                newaccount.Address.Add(new Address()
                {
                    id = item.id,
                    id_Account = id,
                    id_District = item.id_District,
                    id_Province = item.id_Province,
                    Address_District = new Address_District()
                    {
                        id = item.Address_District.id,
                        Name = item.Address_District.Name,
                    },
                    Address_Province = new Address_Province()
                    {
                        id = item.Address_Province.id,
                        Name = item.Address_Province.Name,
                    },
                    AddressDetail = item.AddressDetail,
                });
            }
            IQueryable<Models.view_CartDetails> lWatch;
            lWatch = db.view_CartDetails.Where(x => x.id_Cart == id && x.IsCheck == true);
            var lWatchs = db.view_CartDetails.Where(x => x.id_Cart == id && x.IsCheck == true).GroupBy(x=>x.id_Shop);
            foreach(var cartdetail in lWatchs)
            {
                Models.Orders order = new Orders();
                view_CartDetails view_CartDetails = cartdetail.FirstOrDefault() as view_CartDetails;
                order.id_Shop = view_CartDetails.id_Shop;
                order.id_Accounts = id;
                order.Shops = new Shops()
                {
                    id = view_CartDetails.id_Shop,
                    Name = view_CartDetails.ShopName,
                    UrlAvatar = view_CartDetails.UrlAvatar,
                };
                int SL = 0;
                double Tong = 0;
                foreach(var Watch in cartdetail)
                {
                    order.OrderDetails.Add(new OrderDetails()
                    {
                        id_Watches = Watch.id_Watch,
                        WatchName = Watch.Name,
                        Price = Watch.Price,
                        Count = Watch.SoLuong,
                        Watches = new Watches()
                        {
                            id = Watch.id_Watch,
                            Name = Watch.Name,
                            Url_Image = db.Watches.Find(Watch.id_Watch).Url_Image,
                        },
                    });
                    SL += Watch.SoLuong;
                    Tong += Watch.SoLuong * Watch.Price;
                }
                order.Ship_fee = 15000;
                order.Count = SL;
                order.Sum = Tong + order.Ship_fee;
                newaccount.Orders.Add(order);
            }
            return newaccount;
        }

        //Orders/EnterOrder
        [Route("api/Orders/EnterOrder/{id}")]
        [HttpPost]
        public Models.Accounts EnterOrder(IEnumerable<Models.Discounts> discounts, int id)
        {
            Accounts accounts = db.Accounts.Find(id);
            Accounts newaccount = new Accounts()
            {
                id = accounts.id,
                Fullname = accounts.Fullname,
                Email = accounts.Email,
                Url_Image_Avatar = accounts.Url_Image_Avatar,
                Point = accounts.Point,
            };

            foreach (var item in accounts.Phone)
            {
                newaccount.Phone.Add(item);
            }
            foreach (var item in accounts.Address)
            {

                newaccount.Address.Add(new Address()
                {
                    id = item.id,
                    id_Account = id,
                    id_District = item.id_District,
                    id_Province = item.id_Province,
                    Address_District = new Address_District()
                    {
                        id = item.Address_District.id,
                        Name = item.Address_District.Name,
                    },
                    Address_Province = new Address_Province()
                    {
                        id = item.Address_Province.id,
                        Name = item.Address_Province.Name,
                    },
                    AddressDetail = item.AddressDetail,
                });
            }
            string paymethod = "";
            switch (discounts.FirstOrDefault().Detail.Trim())
            {
                case "beepoint":
                    var watches = db.view_CartDetails.Where(x => x.id_Cart == id && x.IsCheck == true).GroupBy(x => x.id_Shop);
                    double Sum = 0;
                    foreach (var item in watches)
                    { 

                    }

                    break;
                case "momo":
                    break;
                case "atm":
                    break;
                case "direct":
                    paymethod = "Khi nhận hàng";
                    break;
            }
            var lWatchs = db.view_CartDetails.Where(x => x.id_Cart == id && x.IsCheck == true).GroupBy(x => x.id_Shop);
            foreach (var cartdetail in lWatchs)
            {
                Models.Orders order = new Orders();
                order.Payment = paymethod;
                view_CartDetails view_CartDetails = cartdetail.FirstOrDefault();
                order.id_Shop = view_CartDetails.id_Shop;
                order.id_Accounts = id;

                int SL = 0;
                double Tong = 0;
                foreach (var Watch in cartdetail)
                {                   
                    SL += Watch.SoLuong;
                    Tong += Watch.SoLuong * Watch.Price;
                }
                order.Ship_fee = 15000;
                order.Count = SL;
                order.Sum = Tong + order.Ship_fee;
                order.Address_District = accounts.Address.FirstOrDefault().id_District;
                order.Address_Province = accounts.Address.FirstOrDefault().id_Province;
                order.Status = 0;
                order.Date_Create = DateTime.Now;
                Models.Discounts dis = discounts.Where(y => y.id == order.id_Shop).FirstOrDefault();
                if (dis != null)
                {
                    Models.Discounts discount = db.Discounts.Where(x => x.Code == dis.Code).FirstOrDefault();
                    if (discount != null)
                    {
                        order.id_Discount = discount.id;
                    }
                };
                foreach (var Watch in cartdetail)
                {
                    order.OrderDetails.Add( new OrderDetails()
                    {
                        id_Watches = Watch.id_Watch,
                        WatchName = Watch.Name,
                        Price = Watch.Price,
                        Count = Watch.SoLuong,
                        Status = 0,
                    });
                };
                db.Orders.Add(order);
            }
            db.SaveChanges();





            return newaccount;


            /*Models.Shops shops = db.Shops.Find(order.id_Shop);
                Models.Sellers sellers = db.Sellers.Find(shops.id_Master);
                Models.Accounts sellAccount = db.Accounts.Find(sellers.id);
                order.Shops = new Shops()
                {
                    id = shops.id,
                    Name = shops.Name,
                    UrlAvatar = shops.UrlAvatar,
                    Address = shops.Address,
                    Sellers = new Sellers()
                    {
                        id = sellers.id,
                        Accounts = new Accounts()
                        {
                            id = sellAccount.id,
                            Email = sellAccount.Email,
                        }
                    }
                };
                foreach(var phone in sellAccount.Phone)
                {
                    order.Shops.Sellers.Accounts.Phone.Add(new Phone()
                    {
                        id = phone.id,
                        Phone1 = phone.Phone1,
                    });
                };*/
            /*order.OrderDetails.Add(new OrderDetails()
                   {
                       id_Watches = Watch.id_Watch,
                       WatchName = Watch.Name,
                       Price = Watch.Price,
                       Count = Watch.SoLuong,
                       Watches = new Watches()
                       {
                           id = Watch.id_Watch,
                           Name = Watch.Name,
                           Url_Image = db.Watches.Find(Watch.id_Watch).Url_Image,
                       },
                   });*/


                /*order.Discounts = new Discounts()
                {
                    id = discount.id,
                    Code = discount.Code,
                    Detail = discount.Detail,
                };
                double? Value = order.Sum - (order.Sum * discount.Value___) / 100 - discount.Value___1;
                if (Value > discount.TypeDiscounts.Max_Discount)
                {
                    Value = discount.TypeDiscounts.Max_Discount;
                }
                order.Discounts.Value___ = Value;*/

                //order.Shops = db.Shops.Find(order.id_Shop);
                //order.Accounts = null;
                //order.Address_District1 = null;
                //order.Address_Province1 = null;
                ///order.Discounts = null;
                /*db.OrderDetails.Add(new OrderDetails()
                {
                    id_Order = 18,
                });*/
        }


    }
}