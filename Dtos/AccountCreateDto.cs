using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Dtos
{
    public class AccountCreateDto
    {
        [Required]
        [MaxLength(25)]
        public string AccountId { get; set; }
    }
}
