using CareGuide.Models.Enums;

namespace CareGuide.Models.DTOs.PersonDisease
{
    public record PersonDiseaseDto(
        Guid Id,
        Guid PersonId,
        string Name,
        DateOnly? DiagnosisDate,
        DiseaseType DiseaseType,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
