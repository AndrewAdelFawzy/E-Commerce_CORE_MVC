using Task.Core.Entities;

namespace Task.Web.Models
{
    public class HomePageViewModel
    {
        public List<Product>? Products { get; set; }
        public List<Brand>? Brands { get; set; }
    }
}
