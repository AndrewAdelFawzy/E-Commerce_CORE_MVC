using System.ComponentModel.DataAnnotations;
using Task.Core.Consts;

namespace Task.Web.Models
{
    public class ContactUsViewModel
    {
        [MaxLength(100)]
        [RegularExpression(RegexPatterns.CharactersOnlyEnglish, ErrorMessage = Errors.EnglishCharacters)]
        public string FullName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(500)]
        public string Message { get; set; } = null!;
    }
}
