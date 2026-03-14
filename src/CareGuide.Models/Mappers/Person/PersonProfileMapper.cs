using AutoMapper;
using CareGuide.Models.DTOs.Person;

namespace CareGuide.Models.Mappers.Person
{
    public class PersonProfileMapper : Profile
    {
        public PersonProfileMapper()
        {
            CreateMap<CreatePersonDto, Entities.Person>();
            CreateMap<Entities.Person, PersonDto>();
        }
    }
}