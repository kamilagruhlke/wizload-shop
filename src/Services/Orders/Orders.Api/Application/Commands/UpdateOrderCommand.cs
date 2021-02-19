using MediatR;
using System;
using System.Collections.Generic;

namespace Orders.Api.Application.Commands
{
    public class UpdateOrderCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public List<Guid> OrderedProducts { get; set; }

        public string Status { get; set; }

        public decimal ValueNet { get; set; }

        public decimal ValueTax { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }
    }
}
