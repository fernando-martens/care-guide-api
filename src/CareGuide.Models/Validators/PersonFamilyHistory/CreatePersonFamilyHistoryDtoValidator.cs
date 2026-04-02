using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.PersonFamilyHistory;
using FluentValidation;

namespace CareGuide.Models.Validators.PersonFamilyHistory
{
    public class CreatePersonFamilyHistoryDtoValidator : AbstractValidator<CreatePersonFamilyHistoryDto>
    {
        public CreatePersonFamilyHistoryDtoValidator()
        {
            RuleFor(x => x.Relationship)
                .NotEmpty().WithMessage("Relationship is required.")
                .MaximumLength(DatabaseConstants.MaxLengthStandardText)
                .WithMessage($"Relationship must be at most {DatabaseConstants.MaxLengthStandardText} characters.");

            RuleFor(x => x.Diagnosis)
                .NotEmpty().WithMessage("Diagnosis is required.")
                .MaximumLength(DatabaseConstants.MaxLengthStandardText)
                .WithMessage($"Diagnosis must be at most {DatabaseConstants.MaxLengthStandardText} characters.");

            RuleFor(x => x.AgeAtDiagnosis)
                .InclusiveBetween(0, 150)
                .WithMessage("AgeAtDiagnosis must be between 0 and 150.")
                .When(x => x.AgeAtDiagnosis.HasValue);
        }
    }
}