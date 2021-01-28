using FluentValidation;
using Products.Api.Application.Commands;

namespace Products.Api.Application.Validation.CommandValidators
{
    public class CreateProducerCommandValidator : AbstractValidator<CreateProducerCommand>
    {
        public CreateProducerCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Description).NotEmpty();
        }
    }
}
