using Categories.Api.Application.Commands;
using FluentValidation;

namespace Categories.Api.Application.Validation.CommandValidators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
        }
    }
}
