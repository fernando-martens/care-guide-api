using CareGuide.Models.Enums;
using System.Text.Json.Serialization;

namespace CareGuide.Models.DTOs.PersonDisease
{
    public record UpdatePersonDiseaseDto(
        Guid Id,
        string Name,
        DateOnly? DiagnosisDate,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] DiseaseType DiseaseType
    );
}
