using System.ComponentModel.DataAnnotations;
using Task.Core.Enums;


namespace Task.Core.Entities
{
    public class Brand
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public Field Field { get; set; }
        public Country Country { get; set; }
        public string BackgroundImageUrl { get; set; } = null!;
        public string ProfileImageUrl { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
