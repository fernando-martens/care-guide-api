namespace CareGuide.Models.DTOs.PersonAnnotation
{
    public record CreatePersonAnnotationDto(
        string Details,
        string? FileUrl
    );
}
