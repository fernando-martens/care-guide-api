using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.Doctor;
using FluentValidation;

namespace CareGuide.Models.Validators.Doctor
{
    public class UpdateDoctorDtoValidator : AbstractValidator<UpdateDoctorDto>
    {
        public UpdateDoctorDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(DatabaseConstants.MaxLengthStandardText)
                .WithMessage($"Name must not exceed {DatabaseConstants.MaxLengthStandardText} characters.");

            RuleFor(x => x.Details)
                .MaximumLength(DatabaseConstants.MaxLengthLargeText)
                .WithMessage($"Details must not exceed {DatabaseConstants.MaxLengthLargeText} characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Details));
        }
    }
}