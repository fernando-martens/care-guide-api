namespace CareGuide.Models.Validators.PersonAnnotation
{
    using FluentValidation;
    using CareGuide.Models.DTOs.PersonAnnotation;

    public class CreatePersonAnnotationDtoValidator : AbstractValidator<CreatePersonAnnotationDto>
    {
        public CreatePersonAnnotationDtoValidator()
        {
            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage("PersonId is required.")
                .NotEqual(Guid.Empty).WithMessage("PersonId cannot be an empty Guid.");

            RuleFor(x => x.Details)
                .NotEmpty().WithMessage("Details is required.")
                .MaximumLength(1000).WithMessage("Details must be at most 1000 characters.");

            RuleFor(x => x.FileUrl)
                .MaximumLength(255).WithMessage("FileUrl must be at most 255 characters.");
        }
    }
}
