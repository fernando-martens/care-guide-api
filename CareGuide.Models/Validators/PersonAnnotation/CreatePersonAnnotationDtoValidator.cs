namespace CareGuide.Models.Validators.PersonAnnotation
{
    using CareGuide.Models.Constants;
    using CareGuide.Models.DTOs.PersonAnnotation;
    using FluentValidation;

    public class CreatePersonAnnotationDtoValidator : AbstractValidator<CreatePersonAnnotationDto>
    {
        public CreatePersonAnnotationDtoValidator()
        {
            RuleFor(x => x.Details)
                .NotEmpty().WithMessage("Details is required.")
                .MaximumLength(DatabaseConstants.MaxLengthLargeText).WithMessage($"Details must be at most {DatabaseConstants.MaxLengthLargeText} characters.");

            RuleFor(x => x.FileUrl)
                .MaximumLength(DatabaseConstants.MaxLengthStandardText).WithMessage($"FileUrl must be at most {DatabaseConstants.MaxLengthStandardText} characters.");
        }
    }
}
