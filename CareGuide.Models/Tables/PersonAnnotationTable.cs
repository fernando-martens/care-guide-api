using CareGuide.Models.DTOs.PersonAnnotation;

namespace CareGuide.Models.Tables
{
    public class PersonAnnotationTable
    {
        public PersonAnnotationTable() { }
        public PersonAnnotationTable(PersonAnnotationDto personAnnotationDto)
        {
            Id = Guid.NewGuid();
            PersonId = personAnnotationDto.PersonId;
            Details = personAnnotationDto.Details;
            FileUrl = personAnnotationDto.FileUrl;
            Register = DateTime.Now;
        }

        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Details { get; set; }
        public string? FileUrl { get; set; }
        public DateTime Register { get; set; }
        public PersonTable Person { get; set; }
    }
}
