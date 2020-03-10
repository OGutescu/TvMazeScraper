using System;

namespace TvMazeScraper.Domain.Entities
{
    public class Person : BaseEntity
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }

        public TvShow TvShow { get; set; }
        public int? TvShowId { get; set; }
        public int? TvShowIdFk { get; set; }
    }
}