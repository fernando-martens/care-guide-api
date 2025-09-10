namespace CareGuide.Models.DTOs.PersonAnnotation
{
    public class CreatePersonAnnotationDto
    {
        public Guid PersonId { get; set; }
        public string? Details { get; set; }
        public string? FileUrl { get; set; }
    }
}
