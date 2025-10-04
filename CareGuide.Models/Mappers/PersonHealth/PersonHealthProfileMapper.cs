using AutoMapper;
using CareGuide.Models.DTOs.PersonHealth;

namespace CareGuide.Models.Mappers.PersonHealth
{
    public class PersonHealthProfileMapper : Profile
    {
        public PersonHealthProfileMapper()
        {
            CreateMap<Entities.PersonHealth, PersonHealthDto>();
            CreateMap<CreatePersonHealthDto, Entities.PersonHealth>();
            CreateMap<UpdatePersonHealthDto, Entities.PersonHealth>();
        }
    }
}
