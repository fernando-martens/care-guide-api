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
                .MaximumLength(255).WithMessage("Password must not exceed 255 characters.");
        }
    }
}
