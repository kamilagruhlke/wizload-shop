using MediatR;

namespace Products.Api.Application.Commands
{
    public class CreateProducerCommand : IRequest<bool>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
