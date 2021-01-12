using Products.Domain.AggregateModel.ProducerAggregate;
using Xunit;

namespace Products.UnitTests.Domain.AggregateModel.ProducerAggregate
{
    public class ProducerTests
    {
        [Fact]
        public void Producer_Constructor_ProducerCreatedCorrectly()
        {
            var producer = new Producer("TEST NAME", "TEST DESCRIPTION", "Test user");

            Assert.Equal("TEST NAME", producer.Name);
            Assert.Equal("TEST DESCRIPTION", producer.Description);
            Assert.Equal("Test user", producer.CreatedBy);
            Assert.Equal("Test user", producer.UpdatedBy);
        }

        [Fact]
        public void Producer_UpdateName_NameIsUpdated()
        {
            var producer = new Producer("TEST", "TEST", "Test user");

            producer.UpdateName("New name");

            Assert.Equal("New name", producer.Name);
        }

        [Fact]
        public void Producer_UpdateDescription_DescriptionIsUpdated()
        {
            var producer = new Producer("TEST", "TEST", "Test user");

            producer.UpdateDescription("New description");

            Assert.Equal("New description", producer.Description);
        }
    }
}
