using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Dtos
{
    public class AccountCreateStatusDto
    {
        [Required]
        public string success { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        [MaxLength(8)]
        public string password { get; set; }
    }
}
