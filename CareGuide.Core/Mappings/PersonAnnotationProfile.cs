using AutoMapper;
using CareGuide.Models.DTOs.PersonAnnotation;
using CareGuide.Models.Tables;

namespace CareGuide.Core.Mappings
{
    public class PersonAnnotationProfile : Profile
    {
        public PersonAnnotationProfile()
        {
            CreateMap<PersonAnnotation, PersonAnnotationDto>();
        }
    }
}
