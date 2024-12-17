using System.ComponentModel.DataAnnotations;

namespace Task.Web.Models
{
    public class ProductViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double CurrentPrice { get; set; }
        public double? OrignalPrice { get; set; }
        public string Code { get; set; } = null!;
        public int AvailableQuantaty { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string BrandName { get; set; } = null!;
    }
}
