using FluentValidation;
using Products.Api.Application.Commands;

namespace Products.Api.Application.Validation.CommandValidators
{
    public class UpdateProducerCommandValidator : AbstractValidator<UpdateProducerCommand>
    {
        public UpdateProducerCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty();
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Description).NotEmpty();
        }
    }
}
