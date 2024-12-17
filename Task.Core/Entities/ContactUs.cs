using System.ComponentModel.DataAnnotations;


namespace Task.Core.Entities
{
    public class ContactUs
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(500)]
        public string Message { get; set; } = null!;

    }
}
