namespace CareGuide.Models.DTOs.Doctor
{
    public record UpdateDoctorDto(
        Guid Id,
        string Name,
        string? Details
    );
}
