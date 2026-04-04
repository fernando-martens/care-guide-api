namespace CareGuide.Models.DTOs.DoctorSpecialty
{
    public record DoctorSpecialtyDto(
        Guid Id,
        Guid DoctorId,
        string Name
    );
}
