using AutoMapper;
using TvMazeScraper.Domain.Entities;

namespace TvMazeScraper.API.Models
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.BirthdayDate, opts => opts.MapFrom(src => src.Birthday))
                ;
        }
    }
}
