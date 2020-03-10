using System.Collections.Generic;

namespace TvMazeScraper.Domain.Entities
{
    public class TvShow : BaseEntity
    {
        public int ShowId { get; set; }
        public string Name { get; set; }
        public List<Person> Persons { get; set; }
    }
}
