using AutoMapper;
using CareGuide.Models.DTOs.Doctor;

namespace CareGuide.Models.Mappers.Doctor
{
    public class DoctorProfileMapper : Profile
    {
        public DoctorProfileMapper()
        {
            CreateMap<Entities.Doctor, DoctorDto>();
            CreateMap<CreateDoctorDto, Entities.Doctor>();
            CreateMap<UpdateDoctorDto, Entities.Doctor>();
        }
    }
}
