using System.ComponentModel.DataAnnotations;

namespace TvMazeScraper.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}