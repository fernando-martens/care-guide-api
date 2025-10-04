using AutoMapper;
using CareGuide.Models.DTOs.PersonAnnotation;
using CareGuide.Models.Entities;

namespace CareGuide.Models.Mappers
{
    public class PersonAnnotationProfileMapper : Profile
    {
        public PersonAnnotationProfileMapper()
        {
            CreateMap<PersonAnnotation, PersonAnnotationDto>();
            CreateMap<CreatePersonAnnotationDto, PersonAnnotation>();
            CreateMap<UpdatePersonAnnotationDto, PersonAnnotation>();
        }
    }
}
