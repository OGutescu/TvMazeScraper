using AutoMapper;
using TvMazeScraper.Domain.Entities;
using TvMazeScraper.Infrastructure.Models;

namespace TvMazeScraper.Service.MapperProfiles
{
    public class TvShowProfile : Profile
    {
        public TvShowProfile()
        {
            CreateMap<TvShowApiModel, TvShow>()
                .ForMember(dest => dest.ShowId, opts => opts.MapFrom(src => src.Id));

        }
    }
}
