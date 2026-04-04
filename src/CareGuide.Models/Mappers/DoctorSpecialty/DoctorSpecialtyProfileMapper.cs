using AutoMapper;
using CareGuide.Models.DTOs.DoctorSpecialty;

namespace CareGuide.Models.Mappers.DoctorSpecialty
{
    public class DoctorSpecialtyProfileMapper : Profile
    {
        public DoctorSpecialtyProfileMapper()
        {
            CreateMap<Entities.DoctorSpecialty, DoctorSpecialtyDto>();
            CreateMap<CreateDoctorSpecialtyDto, Entities.DoctorSpecialty>();
            CreateMap<UpdateDoctorSpecialtyDto, Entities.DoctorSpecialty>();
        }
    }
}
