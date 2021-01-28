using System;
using MediatR;
using Products.Api.Application.Models;

namespace Products.Api.Application.Commands
{
    public class GetProducerByIdCommand : IRequest<ProducerModel>
    {
        public Guid Id { get; set; }

        public GetProducerByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
