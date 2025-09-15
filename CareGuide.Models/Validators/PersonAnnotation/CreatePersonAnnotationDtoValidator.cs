namespace CareGuide.Models.Validators.PersonAnnotation
{
    using CareGuide.Models.DTOs.PersonAnnotation;
    using FluentValidation;

    public class CreatePersonAnnotationDtoValidator : AbstractValidator<CreatePersonAnnotationDto>
    {
        public CreatePersonAnnotationDtoValidator()
        {
            RuleFor(x => x.Details)
                .NotEmpty().WithMessage("Details is required.")
                .MaximumLength(1000).WithMessage("Details must be at most 1000 characters.");

            RuleFor(x => x.FileUrl)
                .MaximumLength(255).WithMessage("FileUrl must be at most 255 characters.");
        }
    }
}
