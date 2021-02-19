using MediatR;
using System;
using System.Collections.Generic;

namespace Orders.Api.Application.Commands
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public List<Guid> OrderedProducts { get; set; }

        public string Status { get; set; }

        public decimal ValueNet { get; set; }

        public decimal ValueTax { get; set; }

        public Guid UserId { get; set; }
    }
}
