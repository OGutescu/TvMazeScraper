using System.Collections.Generic;

namespace TvMazeScraper.API.Models
{
    public class TvShowModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PersonModel> Cast { get; set; }
    }
}
