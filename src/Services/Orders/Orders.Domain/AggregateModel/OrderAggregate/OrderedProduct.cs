using Orders.Domain.SeedWork;
using System;

namespace Orders.Domain.AggregateModel.OrderAggregate
{
    public class OrderedProduct : IAggregateRoot
    {
        public Guid Id { get; protected set; }

        public Guid ProductId {get; protected set;}

        public OrderedProduct(Guid productId)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
        }

        protected OrderedProduct()
        {
        }
    }
}
