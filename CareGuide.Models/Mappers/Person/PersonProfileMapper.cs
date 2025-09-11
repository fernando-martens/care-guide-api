using AutoMapper;
using CareGuide.Models.DTOs.Person;
using CareGuide.Models.Tables;

namespace CareGuide.Models.Mappers
{
    public class PersonProfileMapper : Profile
    {
        public PersonProfileMapper()
        {
            CreateMap<CreatePersonDto, Person>();
            CreateMap<Person, PersonDto>();
        }
    }
}