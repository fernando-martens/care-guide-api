using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.PersonFamilyHistory;
using FluentValidation;

namespace CareGuide.Models.Validators.PersonFamilyHistory
{
    public class UpdatePersonFamilyHistoryDtoValidator : AbstractValidator<UpdatePersonFamilyHistoryDto>
    {
        public UpdatePersonFamilyHistoryDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .NotEqual(Guid.Empty).WithMessage("Id cannot be an empty Guid.");

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