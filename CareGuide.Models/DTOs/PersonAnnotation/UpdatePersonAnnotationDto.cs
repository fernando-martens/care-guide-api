namespace CareGuide.Models.DTOs.PersonAnnotation
{
    public class UpdatePersonAnnotationDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Details { get; set; }
        public string? FileUrl { get; set; }
    }
}
