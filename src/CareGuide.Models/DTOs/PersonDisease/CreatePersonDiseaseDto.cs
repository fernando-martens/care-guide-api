using CareGuide.Models.Enums;
using System.Text.Json.Serialization;

namespace CareGuide.Models.DTOs.PersonDisease
{
    public record CreatePersonDiseaseDto(
        string Name,
        DateOnly? DiagnosisDate,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] DiseaseType DiseaseType
    );
}
