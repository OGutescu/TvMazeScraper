using System;
using System.ComponentModel.DataAnnotations;

namespace TvMazeScraper.Domain.Entities
{
    public class Bookmark
    {
        [Key]
        public int PageNumber { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdate { get; set; } 
    }
}
