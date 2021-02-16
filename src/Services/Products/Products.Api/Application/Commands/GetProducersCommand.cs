using System;
using System.Collections.Generic;
using MediatR;
using Products.Api.Application.Models;

namespace Products.Api.Application.Commands
{
    public class GetProducersCommand : IRequest<List<ProducerModel>>
    {
    }
}
