using FluentValidation;
using Products.Api.Application.Commands;

namespace Products.Api.Application.Validation.CommandValidators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Description).NotEmpty();
            RuleFor(e => e.Image).NotEmpty();
            RuleFor(e => e.ProducerCode).NotEmpty();
            RuleFor(e => e.ProducerId).NotEmpty();
            RuleFor(e => e.CategoryId).NotEmpty();
            RuleFor(e => e.Specification).NotEmpty();
        }
    }
}
