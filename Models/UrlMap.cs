using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class UrlMap
    {
        [Key]
        public int UrlMapId { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string ShortUrl { get; set; }
        public int RedirectType { get; set; }
        public int TimesVisited { get; set; }

        // Foreign key
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public UrlMap()
        {
            TimesVisited = 0;
        }
    }
}
