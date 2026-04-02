namespace CareGuide.Models.DTOs.PersonFamilyHistory
{
    public record PersonFamilyHistoryDto(
        Guid Id,
        Guid PersonId,
        string Relationship,
        string Diagnosis,
        int? AgeAtDiagnosis
    );
}