using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.PersonDisease
{
    public record UpdatePersonDiseaseDto(
        Guid Id,
        string Name,
        DateOnly? DiagnosisDate,
        DiseaseType DiseaseType
    );
}
