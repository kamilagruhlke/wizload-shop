using System;
using MediatR;

namespace Products.Api.Application.Commands
{
    public class UpdateProducerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
