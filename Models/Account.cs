using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        [MaxLength(25)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        public ICollection<UrlMap> UrlMaps { get; set; }
    }
}
