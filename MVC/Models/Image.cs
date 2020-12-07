using System;

namespace MVC.Models
{
    public class Image
    {
        public int id { get; set; }
        public Nullable<int> id_Watches { get; set; }
        public string Url_Image { get; set; }

        public virtual Accounts Accounts { get; set; }
    }
}