using MediatR;
using Orders.Api.Application.Models;
using System;
using System.Collections.Generic;

namespace Orders.Api.Application.Commands
{
    public class GetCreatedOrdersCommand : IRequest<List<OrderModel>>
    {
        public DateTime Date { get; set; }
    }
}
