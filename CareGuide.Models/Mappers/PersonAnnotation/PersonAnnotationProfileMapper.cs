using AutoMapper;
using CareGuide.Models.DTOs.PersonAnnotation;

namespace CareGuide.Models.Mappers.PersonAnnotation
{
    public class PersonAnnotationProfileMapper : Profile
    {
        public PersonAnnotationProfileMapper()
        {
            CreateMap<Entities.PersonAnnotation, PersonAnnotationDto>();
            CreateMap<CreatePersonAnnotationDto, Entities.PersonAnnotation>();
            CreateMap<UpdatePersonAnnotationDto, Entities.PersonAnnotation>();
        }
    }
}
