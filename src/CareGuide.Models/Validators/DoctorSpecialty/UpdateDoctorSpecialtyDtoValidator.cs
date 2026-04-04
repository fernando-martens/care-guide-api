using CareGuide.Models.Constants;
using CareGuide.Models.DTOs.DoctorSpecialty;
using FluentValidation;

namespace CareGuide.Models.Validators.DoctorSpecialty
{
    public class UpdateDoctorSpecialtyDtoValidator : AbstractValidator<UpdateDoctorSpecialtyDto>
    {
        public UpdateDoctorSpecialtyDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(DatabaseConstants.MaxLengthStandardText)
                .WithMessage($"Name must not exceed {DatabaseConstants.MaxLengthStandardText} characters.");
        }
    }
}
