using FluentValidation;
using HelpDeskAPI.DTOs;

namespace HelpDeskAPI.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100);
        }
    }

    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name cannot be blank if provided.")
                .MaximumLength(100)
                .When(x => x.Name is not null);
        }
    }
}