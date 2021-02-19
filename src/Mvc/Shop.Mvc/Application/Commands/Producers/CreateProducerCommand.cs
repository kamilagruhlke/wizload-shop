using MediatR;

namespace Shop.Mvc.Application.Commands.Producers
{
    public class CreateProducerCommand : IRequest<bool>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Saved { get; set; } = false;
    }
}
