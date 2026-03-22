using CareGuide.Models.Enums;
using System.Text.Json.Serialization;

namespace CareGuide.Models.DTOs.PersonDisease
{
    public record PersonDiseaseDto(
        Guid Id,
        Guid PersonId,
        string Name,
        DateOnly? DiagnosisDate,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] DiseaseType DiseaseType,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
