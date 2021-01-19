using API_Local.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace API_Local.Controllers
{
    public class HomeViewController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();
        [Route("api/lWatchHome")]
        [HttpPost]
        [ResponseType(typeof(ViewModels.AllHomeViewModel))]
        public IHttpActionResult lWatchHome(ViewModels.AllHomeViewModel allHomeViewModel)
        {
            ViewModels.HomeViewModel homeViewModel = allHomeViewModel.homeViewModel;

            IQueryable<Models.view_WatchDetails> result;

            if (homeViewModel.search != "")
            {
                result = db.view_WatchDetails.AsNoTracking().Where(x => x.Name.IndexOf(homeViewModel.search) >= 0 || x.id.ToString().IndexOf(homeViewModel.search) >= 0);
            }
            else
            {
                result = db.view_WatchDetails.AsNoTracking();
            }

            foreach(var item in result)
            {
                if (item.Url_Image == null)
                {
                    Image temp = db.Image.Where(x => x.id_Watches == item.id).FirstOrDefault();
                    if(temp != null)
                    {
                        item.Url_Image = temp.Url_Image;
                    }
                }
            }

            //Lọc theo giá
            switch (homeViewModel.valueWatch)
            {
                case 0:
                    break;
                case 1://<500.000
                    result = result.Where(x => x.Price < 500000);
                    break;
                case 2://500.000<= x < 1.000.000
                    result = result.Where(x => x.Price >= 500000 && x.Price < 1000000);
                    break;
                case 3://1.000.000<= x < 2.000.000
                    result = result.Where(x => x.Price >= 1000000 && x.Price < 2000000);
                    break;
                case 4://2.000.000<= x < 3.000.000
                    result = result.Where(x => x.Price >= 2000000 && x.Price < 3000000);
                    break;
                case 5://>=3.000.000
                    result = result.Where(x => x.Price >= 3000000);
                    break;
            }

            //Lọc theo hãng
            if (homeViewModel.idFirm != 0)
            {
                result = result.Where(x => x.id_Firms == homeViewModel.idFirm);
            }

            //Lọc theo loại////////////////////////
            if (homeViewModel.idCategori != 0)
            {
                string temp = db.view_Categories.Where(x => x.id == homeViewModel.idCategori).FirstOrDefault().Name;
                result = result.Where(x => x.Name_Categories.IndexOf(temp) >= 0);
            }

            //Lọc theo giới tính
            if (homeViewModel.idSex != 0)
            {
                result = result.Where(x => x.id_Sex == homeViewModel.idSex);
            }

            //Sắp xếp
            switch (homeViewModel.sortBy)
            {
                case 0://hot mua nhiều
                    result = result.OrderByDescending(x => x.LuotMua);
                    break;
                case 1://rate cao
                    result = result.OrderByDescending(x => x.Rate);
                    break;
                case 2://Giảm dần giá
                    result = result.OrderBy(x => x.Price);
                    break;
                case 3://Tăng dần giá
                    result = result.OrderByDescending(x => x.Price);
                    break;
                case 4://Mới nhất
                    result = result.OrderByDescending(x => x.Date_Create);
                    break;
            }

            allHomeViewModel.view_WatchDetails = result.Skip(((homeViewModel.numPage - 1) * 40)).Take(40);

            return Ok(allHomeViewModel);
        }
    }
}
