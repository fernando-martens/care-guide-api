using AutoMapper;
using CareGuide.Models.DTOs.PersonDisease;

namespace CareGuide.Models.Mappers.PersonDisease
{
    public class PersonDiseaseProfileMapper : Profile
    {
        public PersonDiseaseProfileMapper()
        {
            CreateMap<Entities.PersonDisease, PersonDiseaseDto>();
            CreateMap<PersonDiseaseDto, Entities.PersonDisease>();
            CreateMap<CreatePersonDiseaseDto, Entities.PersonDisease>();
            CreateMap<UpdatePersonDiseaseDto, Entities.PersonDisease>();
        }
    }
}
