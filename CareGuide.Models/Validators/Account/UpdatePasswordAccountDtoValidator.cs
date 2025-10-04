using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.Auth;
using FluentValidation;

namespace CareGuide.Models.Validators.Account
{
    public class UpdatePasswordAccountDtoValidator : AbstractValidator<UpdatePasswordAccountDto>
    {
        public UpdatePasswordAccountDtoValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(DatabaseConstants.MaxLengthStandardText).WithMessage($"Password must not exceed {DatabaseConstants.MaxLengthStandardText} characters.");
        }
    }
}
