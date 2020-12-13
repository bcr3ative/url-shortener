using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Dtos
{
    public class UrlMapCreateDto
    {
        [Required]
        public string url { get; set; }
        public int redirectType { get; set; }
    }
}
