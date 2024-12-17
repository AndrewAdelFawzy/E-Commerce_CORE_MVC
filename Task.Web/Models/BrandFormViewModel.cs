using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Task.Core.Enums;

namespace Task.Web.Models
{
    public class BrandFormViewModel
    {
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public Field Field { get; set; }
        public List<SelectListItem> Fields { get; set; } = new List<SelectListItem>();
        public Country Country { get; set; }
        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();
        public IFormFile? BackgroundImage { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
