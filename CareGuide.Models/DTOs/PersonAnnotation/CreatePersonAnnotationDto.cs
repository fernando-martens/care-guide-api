namespace CareGuide.Models.DTOs.PersonAnnotation
{
    public record CreatePersonAnnotationDto(
        Guid PersonId,
        string Details,
        string? FileUrl
    );
}
