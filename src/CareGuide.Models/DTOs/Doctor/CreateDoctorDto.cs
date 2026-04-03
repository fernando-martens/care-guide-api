namespace CareGuide.Models.DTOs.Doctor
{
    public record CreateDoctorDto(
        string Name,
        string? Details
    );
}
