using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Task.Core.Consts;

namespace Task.Web.Models
{
    public class ProductFormViewModel
    {
        [MaxLength(100)]
        [Display(Name ="Product Title")]
        public string Title { get; set; } = null!;
        [MaxLength(200)]
        [Display(Name = "Product description")]
        public string Description { get; set; } = null!;

        [Display(Name = "Current price")]
        public double CurrentPrice { get; set; }

        [Display(Name = "Orignal price")]
        public double? OrignalPrice { get; set; }

        [MaxLength(9)]
        [Remote("AllowProductCode", null!, AdditionalFields = "Id", ErrorMessage = Errors.Dublicated)]
        public string Code { get; set; } = null!;

        [Display(Name = "Available Quantaty")]
        public int AvailableQuantaty { get; set; }
        public IFormFile Image { get; set; } = null!;
        public Guid BrandId { get; set; }

        public List<SelectListItem>? Brands { get; set; } 
    }
}
