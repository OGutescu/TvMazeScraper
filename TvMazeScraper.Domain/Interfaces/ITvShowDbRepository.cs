using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScraper.Domain.Entities;

namespace TvMazeScraper.Domain.Interfaces
{
    public interface ITvShowDbRepository
    {
        Task<IEnumerable<TvShow>> GetTvShowsForPage(int pageNo, int pageSize);
        Task AddTvShowsWithPersons(List<TvShow> tvShows);
    }
}