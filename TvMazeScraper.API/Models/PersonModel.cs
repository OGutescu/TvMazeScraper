using System;
using System.Text.Json.Serialization;

namespace TvMazeScraper.API.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public DateTime? BirthdayDate { get; set; }
        public string Birthday => BirthdayDate.HasValue ? BirthdayDate.Value.ToString("yyyy-MM-dd") : string.Empty;
    }
}