using AutoMapper;
using CareGuide.Models.DTOs.PersonAnnotation;
using CareGuide.Models.Tables;

namespace CareGuide.Models.Mappers
{
    public class PersonAnnotationProfileMapper : Profile
    {
        public PersonAnnotationProfileMapper()
        {
            CreateMap<PersonAnnotation, PersonAnnotationDto>();
        }
    }
}
