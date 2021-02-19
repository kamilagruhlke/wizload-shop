using MediatR;
using System;

namespace Shop.Mvc.Application.Commands.Orders
{
    public class CreateOrderCommand : IRequest<bool>
    {
        public Guid BaksetId { get; set; }

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
