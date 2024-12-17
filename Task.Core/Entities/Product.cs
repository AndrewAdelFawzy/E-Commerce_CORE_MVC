using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Task.Core.Entities
{
    [Index(nameof(Code), IsUnique = true)]
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(100)]
        public string Title { get; set; } = null!;
        [MaxLength(200)]
        public string Description { get; set; } = null!;
        public double CurrentPrice { get; set; }
        public double? OrignalPrice { get; set; }
        public string Code { get; set; } = null!;
        public int AvailableQuantaty { get; set; }
        public string ImageUrl { get; set; } = null!;

        public Guid BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
    }
}
