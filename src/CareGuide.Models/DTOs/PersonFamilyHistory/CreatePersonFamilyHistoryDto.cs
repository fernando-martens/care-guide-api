namespace CareGuide.Models.DTOs.PersonFamilyHistory
{
    public record CreatePersonFamilyHistoryDto(
        string Relationship,
        string Diagnosis,
        int? AgeAtDiagnosis
    );
}