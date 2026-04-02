using AutoMapper;
using CareGuide.Models.DTOs.PersonFamilyHistory;

namespace CareGuide.Models.Mappers.PersonFamilyHistory
{
    public class PersonFamilyHistoryProfileMapper : Profile
    {
        public PersonFamilyHistoryProfileMapper()
        {
            CreateMap<Entities.PersonFamilyHistory, PersonFamilyHistoryDto>();
            CreateMap<CreatePersonFamilyHistoryDto, Entities.PersonFamilyHistory>();
            CreateMap<UpdatePersonFamilyHistoryDto, Entities.PersonFamilyHistory>();
        }
    }
}
