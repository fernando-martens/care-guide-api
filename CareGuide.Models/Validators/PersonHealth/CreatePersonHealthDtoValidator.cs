using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.PersonHealth;
using FluentValidation;

namespace CareGuide.Models.Validators.PersonHealth
{
    public class CreatePersonHealthDtoValidator : AbstractValidator<CreatePersonHealthDto>
    {
        public CreatePersonHealthDtoValidator()
        {
            RuleFor(x => x.BloodType)
                .IsInEnum().WithMessage("Invalid blood type.");

            RuleFor(x => x.Height)
                .GreaterThan(0).WithMessage("Height must be greater than 0.")
                .LessThanOrEqualTo(3).WithMessage("Height must be less than or equal to 3.00 meters.");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0.")
                .LessThanOrEqualTo(500).WithMessage("Weight must be less than or equal to 500 kg.");

            RuleFor(x => x.Description)
                .MaximumLength(DatabaseConstants.MaxLengthLargeText)
                .When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage($"Description must not exceed {DatabaseConstants.MaxLengthLargeText} characters.");
        }
    }
}