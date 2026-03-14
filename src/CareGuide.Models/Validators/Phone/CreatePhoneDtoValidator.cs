using CareGuide.Models.DTOs.Phone;
using CareGuide.Models.Enums;
using FluentValidation;

namespace CareGuide.Models.Validators.Phone
{
    public class CreatePhoneDtoValidator : AbstractValidator<CreatePhoneDto>
    {
        public CreatePhoneDtoValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{8,12}$").WithMessage("Phone number must contain only digits and be between 8 and 12 characters long.");

            RuleFor(x => x.AreaCode)
                .NotEmpty().WithMessage("Area code is required.")
                .Matches(@"^\d{2,5}$").WithMessage("Area code must contain only digits and be between 2 and 5 characters long.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Phone type is invalid.")
                .Must(type => Enum.IsDefined(typeof(PhoneType), type))
                .WithMessage("Phone type must be one of: R, COM, CEL, or O.");
        }
    }
}
