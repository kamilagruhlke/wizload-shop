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

        public Guid UserId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }
}
