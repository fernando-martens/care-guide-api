namespace CareGuide.Models.DTOs.PersonAnnotation
{
    public record UpdatePersonAnnotationDto(
        Guid Id,
        Guid PersonId,
        string Details,
        string? FileUrl
    );
}
