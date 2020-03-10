using AutoMapper;
using TvMazeScraper.Domain.Entities;

namespace TvMazeScraper.API.Models
{
    public class TvShowProfile : Profile
    {
        public TvShowProfile()
        {
            CreateMap<TvShow, TvShowModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.ShowId))
                .ForMember(dest => dest.Cast, opts => opts.MapFrom(src => src.Persons))
                ;

        }
    }
}
