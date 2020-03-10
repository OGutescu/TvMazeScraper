using AutoMapper;
using TvMazeScraper.Domain.Entities;
using TvMazeScraper.Infrastructure.Models;

namespace TvMazeScraper.Service.MapperProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonApiModel, Person>()
                .ForMember(dest=>dest.PersonId, opts=>opts.MapFrom(src=>src.Id));
            
        }
    }
}
