﻿using System.ComponentModel.DataAnnotations;

namespace simple.rss.reader.Models
{
    public class Feed
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
