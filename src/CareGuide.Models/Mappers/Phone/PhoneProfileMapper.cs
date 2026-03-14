using AutoMapper;
using CareGuide.Models.DTOs.Phone;

namespace CareGuide.Models.Mappers.Phone
{
    public class PhoneProfileMapper : Profile
    {
        public PhoneProfileMapper()
        {
            CreateMap<Entities.Phone, PhoneDto>();
            CreateMap<CreatePhoneDto, Entities.Phone>();
            CreateMap<UpdatePhoneDto, Entities.Phone>();
        }
    }
}
