namespace CareGuide.Models.Validators.PersonAnnotation
{
    using CareGuide.Models.DTOs.PersonAnnotation;
    using FluentValidation;

    public class UpdatePersonAnnotationDtoValidator : AbstractValidator<UpdatePersonAnnotationDto>
    {
        public UpdatePersonAnnotationDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .NotEqual(Guid.Empty).WithMessage("Id cannot be an empty Guid.");

            RuleFor(x => x.Details)
                .NotEmpty().WithMessage("Details is required.")
                .MaximumLength(1000).WithMessage("Details must be at most 1000 characters.");

            RuleFor(x => x.FileUrl)
                .MaximumLength(255).WithMessage("FileUrl must be at most 255 characters.");
        }
    }
}
