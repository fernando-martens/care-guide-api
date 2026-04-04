using AutoMapper;
using CareGuide.Models.DTOs.DoctorPhone;

namespace CareGuide.Models.Mappers.DoctorPhone
{
    public class DoctorPhoneProfileMapper : Profile
    {
        public DoctorPhoneProfileMapper()
        {
            CreateMap<Entities.DoctorPhone, DoctorPhoneDto>()
                .ForCtorParam(nameof(DoctorPhoneDto.DoctorId), opt => opt.MapFrom(src => src.DoctorId))
                .ForCtorParam(nameof(DoctorPhoneDto.PhoneId), opt => opt.MapFrom(src => GetPhone(src).Id))
                .ForCtorParam(nameof(DoctorPhoneDto.Number), opt => opt.MapFrom(src => GetPhone(src).Number))
                .ForCtorParam(nameof(DoctorPhoneDto.AreaCode), opt => opt.MapFrom(src => GetPhone(src).AreaCode))
                .ForCtorParam(nameof(DoctorPhoneDto.Type), opt => opt.MapFrom(src => GetPhone(src).Type));
        }

        private static Entities.Phone GetPhone(Entities.DoctorPhone source)
        {
            return source.Phone
                ?? throw new InvalidOperationException("Phone not found.");
        }
    }
}
