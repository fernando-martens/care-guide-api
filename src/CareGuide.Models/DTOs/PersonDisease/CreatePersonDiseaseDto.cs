using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.PersonDisease
{
    public record CreatePersonDiseaseDto(
        string Name,
        DateOnly? DiagnosisDate,
        DiseaseType DiseaseType
    );
}
