using System;

namespace TvMazeScraper.Infrastructure.Models
{
    public class TvShowApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CastApiModel
    {
        public PersonApiModel Person { get; set; }
    }

    public class PersonApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
