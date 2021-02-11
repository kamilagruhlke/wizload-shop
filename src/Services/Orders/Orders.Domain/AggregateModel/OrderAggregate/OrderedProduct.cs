using Orders.Domain.SeedWork;
using System;

namespace Orders.Domain.AggregateModel.OrderAggregate
{
    public class OrderedProduct : IAggregateRoot
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public decimal NetPrice { get; protected set; }

        public decimal Tax { get; protected set; }
    }
}
