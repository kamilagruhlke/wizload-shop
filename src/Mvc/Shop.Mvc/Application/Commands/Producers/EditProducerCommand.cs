using MediatR;
using System;

namespace Shop.Mvc.Application.Commands.Producers
{
    public class EditProducerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Saved { get; set; } = false;
    }
}
