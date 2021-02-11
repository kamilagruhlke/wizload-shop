using Orders.Domain.AggregateModel.OrderAggregate;
using Orders.Domain.Exceptions;
using Orders.Domain.Utils.Dictionaries;
using System;
using Xunit;

namespace Orders.UnitTests.Domain.AggregateModel.OrderAggregate
{
    public class OrderTests
    {
        [Fact]
        public void Order_Constructor_OrderCreatedCorrectly()
        {
            var userId = Guid.NewGuid();
            var order = new Order(10.0m, 2.30m, userId, "user test");

            Assert.Equal(StatusDictionary.Pending, order.Status);
            Assert.Equal(10.0m, order.ValueNet);
            Assert.Equal(2.30m, order.ValueTax);
            Assert.Equal("user test", order.CreatedBy);
            Assert.Null(order.UpdateBy);
        }

        [Fact]
        public void Order_UpdateStatus_StatusIsUpdated()
        {
            var order = new Order(10.0m, 2.30m, Guid.NewGuid(), "user test");

            order.UpdateStatus(StatusDictionary.InProgress, "user");

            Assert.Equal(StatusDictionary.InProgress, order.Status);
            Assert.Equal("user", order.UpdateBy);
        }

        [Fact]
        public void Order_UpdateStatus_StatusIsNotUpdated()
        {
            var order = new Order(10.0m, 2.30m, Guid.NewGuid(), "user test");

            Assert.Throws<UnknownDictionaryKeyBusinessException>(() => {
                order.UpdateStatus("BAD_STATUS", "user");
            });
        }
    }
}
