using CareGuide.Models.DTOs.Auth;
using FluentValidation;

namespace CareGuide.Models.Validators
{
    public class CreateAccountDtoValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(255).WithMessage("Email must not exceed 255 characters.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(255).WithMessage("Password must not exceed 255 characters.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name must not exceed 255 characters.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Gender must be 'M', 'F', or 'O'.");

            RuleFor(x => x.Birthday)
                .NotEmpty().WithMessage("Birthday is required.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Birthday cannot be in the future.");
        }
    }
}
