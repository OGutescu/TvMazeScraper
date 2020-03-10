using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TvMazeScraper.API.Models;
using TvMazeScraper.Domain.Interfaces;

namespace TvMazeScraper.API.Repository
{
    public interface IRepository
    {
        Task<List<TvShowModel>> GetTvShowsPerPage(int pageNr, int pageSize);
    }
    public class Repository : IRepository
    {
        private readonly ITvShowDbRepository _dbRepository;
        private readonly IMapper _mapper;

        public Repository(ITvShowDbRepository dbRepository, IMapper mapper)
        {
            _dbRepository = dbRepository;
            _mapper = mapper;
        }

        public async Task<List<TvShowModel>> GetTvShowsPerPage(int pageNr, int pageSize)
        {
            var shows = await _dbRepository.GetTvShowsForPage(pageNr, pageSize);

            if (!shows.Any())
                return new List<TvShowModel>();
            
            return shows.Select(s => _mapper.Map<TvShowModel>(s)).ToList();
        }
    }
}
