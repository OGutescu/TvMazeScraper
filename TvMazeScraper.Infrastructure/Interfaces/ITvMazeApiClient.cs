using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScraper.Infrastructure.Models;

namespace TvMazeScraper.Infrastructure.Interfaces
{
    public interface ITvMazeApiClient
    {
        Task<List<TvShowApiModel>> GetTvShowsFromApi(int pageNo);
        Task<List<CastApiModel>> GetCastFromApi(int tvShowId);
    }
}