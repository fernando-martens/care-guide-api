namespace CareGuide.Models.DTOs.PersonAnnotation
{
    public record UpdatePersonAnnotationDto(
        Guid Id,
        string Details,
        string? FileUrl
    );
}
