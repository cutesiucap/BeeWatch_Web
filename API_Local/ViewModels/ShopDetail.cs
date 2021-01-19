using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Local.ViewModels
{
    public class ShopDetail
    {
        public Shop shop { get; set; }
        public ICollection<Danhgia> danhgias { get; set; }
        public ICollection<Models.view_WatchDetails> Sanphamhot { get; set; }
        public ICollection<Models.view_WatchDetails> Tatca { get; set; }
    }
}