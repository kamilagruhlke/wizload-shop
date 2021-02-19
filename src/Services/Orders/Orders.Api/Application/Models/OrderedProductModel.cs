using System;

namespace Orders.Api.Application.Models
{
    public class OrderedProductModel
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
    }
}
