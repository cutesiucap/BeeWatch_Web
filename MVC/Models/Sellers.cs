namespace MVC.Models
{
    public class Sellers
    {
        public int id { get; set; }
        public string Identitycard { get; set; }
        public string Bankcard { get; set; }
        public string Url_Image_Cover { get; set; }

        public virtual Accounts Accounts { get; set; }
    }
}