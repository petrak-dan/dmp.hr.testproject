using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace simple.rss.reader.Models
{
    public class Feed
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        [Required]
        [DisplayName("URL")]
        [Url]
        public string Url { get; set; } = "";

        [Required]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
