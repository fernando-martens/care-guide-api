namespace CareGuide.Models.DTOs.PersonAnnotation
{
    public record PersonAnnotationDto(
        Guid Id,
        Guid PersonId,
        string Details,
        string? FileUrl
    );
}
