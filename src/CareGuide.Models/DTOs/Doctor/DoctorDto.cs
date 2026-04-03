namespace CareGuide.Models.DTOs.Doctor
{
    public record DoctorDto(
        Guid Id,
        Guid PersonId,
        string Name,
        string? Details,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
