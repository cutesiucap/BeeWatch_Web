using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API_Local.Models;

namespace API_Local.Controllers
{
    public class SellerViewModelsController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();
        [Route("api/sellercard/{id}")]
        [HttpGet]
        [ResponseType(typeof(ViewModels.SellerViewModels))]
        public IHttpActionResult SellerView(int id)
        {
            ViewModels.SellerViewModels SellerView = new ViewModels.SellerViewModels();
            SellerView.New_Order = db.Orders.Where(x => x.Status == 0).Count();
            SellerView.Average_Rate = db.OrderDetails.Where(x => x.Rate != null && x.Status == 3).Average(x => x.Rate);
            SellerView.Sum_Customer = db.Orders.Where(x => x.id_Shop == id).GroupBy(x => x.id_Accounts).Count();
            SellerView.Sum_Watches = db.Watches.Where(x => x.id_Shop == id && x.IsExist == true).Count();
            return Ok(SellerView);

        }
    }
}
