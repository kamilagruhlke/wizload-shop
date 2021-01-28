using System;
using MediatR;

namespace Products.Api.Application.Commands
{
    public class GetProducerByIdCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public GetProducerByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
