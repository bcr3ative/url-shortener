using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Dtos
{
    public class AccountLoginRequestDto
    {
        [Required]
        [MaxLength(25)]
        public string AccountId { get; set; }
        [Required]
        [MaxLength(8)]
        public string Password { get; set; }
    }
}
