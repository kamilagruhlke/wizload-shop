using Categories.Api.Application.Commands;
using FluentValidation;

namespace Categories.Api.Application.Validation.CommandValidators
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
        }
    }
}
