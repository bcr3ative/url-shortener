using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Dtos
{
    public class UrlMapReadDto
    {
        [Required]
        public string shortUrl { get; set; }
    }
}
