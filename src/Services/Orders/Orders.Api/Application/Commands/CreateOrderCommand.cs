using MediatR;
using System;
using System.Collections.Generic;

namespace Orders.Api.Application.Commands
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public List<Guid> OrderedProducts { get; set; }

        public decimal ValueNet { get; set; }

        public decimal ValueTax { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string ClientFullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
