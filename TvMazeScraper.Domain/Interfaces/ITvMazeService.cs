using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScraper.Domain.Entities;

namespace TvMazeScraper.Domain.Interfaces
{
    public interface ITvMazeService
    {
        Task<List<TvShow>> GetTvShows(int pageNo);
        Task<List<Person>> GetCastForShow(int tvShowId);
    }
}