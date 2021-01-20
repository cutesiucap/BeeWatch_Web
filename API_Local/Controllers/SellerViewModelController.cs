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
    public class SellerViewModelController : ApiController
    {
        private BeeWatchDBEntities DB = new BeeWatchDBEntities();


        [Route("api/SellerViewModels/{id}" )]
        [HttpGet]
        [ResponseType(typeof(ViewModels.SellerViewModel))]
        public IHttpActionResult SellerView(int id)
        {
            ViewModels.SellerViewModel sellerviewmodel = new ViewModels.SellerViewModel();
            int count_new_order = DB.Orders.Where(x => x.Status != 0 && x.id_Shop == id && x.Status != null).Count() ;

            List<OrderDetails> orderDetails = new List<OrderDetails>();
            foreach(var item in DB.Orders.Where(x => x.id_Shop == id))
            {
                foreach(var i in item.OrderDetails)
                {
                    orderDetails.Add(i);
                }
            }
            double? average_rate = orderDetails.AsEnumerable().Average(x => x.Rate);
            List<int?> listcusomer = new List<int?>();
            foreach (var item in DB.Orders.Where(x => x.id_Shop == id))
            {
                listcusomer.Add(item.id_Accounts);
            }
            for (int i = 0; i < listcusomer.Count() - 1; i++)
                for (int j = i + 1; j < listcusomer.Count() - 2; j++)
                    if (listcusomer[j] == listcusomer[i])
                        listcusomer[j] = -1;
            int sum_customer = 0;
            for (int i = 0; i < listcusomer.Count(); i++)
                if (listcusomer[i] > 0)
                    sum_customer++;
            int sum_watches = DB.Watches.Where(x => x.id_Shop == id && x.IsExist == true).Count();
            sellerviewmodel.Sum_New_Order = count_new_order;
            if(average_rate is null)
            {
                sellerviewmodel.Average_Rate = 0;
            }
            else
                sellerviewmodel.Average_Rate = average_rate;

            sellerviewmodel.Sum_Customer = sum_customer;
            sellerviewmodel.Sum_Watches = sum_watches;
            return Ok(sellerviewmodel);
        }
        
    } 
}
