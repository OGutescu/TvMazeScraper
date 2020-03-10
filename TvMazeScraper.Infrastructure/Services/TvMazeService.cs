using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TvMazeScraper.Domain.Entities;
using TvMazeScraper.Domain.Interfaces;
using TvMazeScraper.Infrastructure.Interfaces;

namespace TvMazeScraper.Infrastructure.Services
{
    public class TvMazeService : ITvMazeService
    {
        private readonly ITvMazeApiClient _tvMazeApiClient;
        private readonly IMapper _mapper;

        public TvMazeService(ITvMazeApiClient tvMazeApiClient, IMapper mapper)
        {
            _tvMazeApiClient = tvMazeApiClient;
            _mapper = mapper;
        }

        public async Task<List<TvShow>> GetTvShows(int pageNo)
        {
            var showsApi = await _tvMazeApiClient.GetTvShowsFromApi(pageNo);
            return showsApi.Select(s=>_mapper.Map<TvShow>(s)).ToList();
        }

        public async Task<List<Person>> GetCastForShow(int tvShowId)
        {
            var apiModels = await _tvMazeApiClient.GetCastFromApi(tvShowId);
            var persons = apiModels.Select(s => _mapper.Map<Person>(s.Person)).ToList();
            persons.ForEach(p => p.TvShowId = tvShowId);
            return persons;
        }
    }
}
