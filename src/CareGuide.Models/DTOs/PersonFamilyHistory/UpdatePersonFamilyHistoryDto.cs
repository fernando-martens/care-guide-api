namespace CareGuide.Models.DTOs.PersonFamilyHistory
{
    public record UpdatePersonFamilyHistoryDto(
        Guid Id,
        string Relationship,
        string Diagnosis,
        int? AgeAtDiagnosis
    );
}
