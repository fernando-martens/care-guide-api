using AutoMapper;
using CareGuide.Models.DTOs.PersonPhone;

namespace CareGuide.Models.Mappers.PersonPhone
{
    public class PersonPhoneProfileMapper : Profile
    {
        public PersonPhoneProfileMapper()
        {
            CreateMap<Entities.PersonPhone, PersonPhoneDto>();
        }
    }
}
