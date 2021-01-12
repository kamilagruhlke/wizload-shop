using Xunit;
using Products.Domain.AggregateModel.ProductAggregate;
using System;

namespace Products.UnitTests.Domain.AggregateModel.ProductAggregate
{
    public class ProductTests
    {

        [Fact]
        public void Product_Constructor_ProductCreatedCorrectly()
        {
            var producerId = Guid.NewGuid();
            var product = new Product("Test name", "Test description", "Test specification", "Test url", producerId, "Test code", "test user");

            Assert.Equal("Test name", product.Name);
            Assert.Equal("Test description", product.Description);
            Assert.Equal("Test specification", product.Specification);
            Assert.Equal("Test url", product.Image);
            Assert.False(product.IsDeleted);
            Assert.Equal(producerId, product.ProducerId);
            Assert.Equal("Test code", product.ProducerCode);
            Assert.Equal("test user", product.CreatedBy);
            Assert.Equal("test user", product.UpdatedBy);
        }

        [Fact]
        public void Product_UpdateName_NameIsUpdated()
        {
            var product = new Product("Test name", "Test", "Test", "Test url", Guid.NewGuid(), "Test code", "test user");

            product.UpdateName("new test name");

            Assert.Equal("new test name", product.Name);
        }

        [Fact]
        public void Product_UpdateSpecification_SpecyficationIsUpdated()
        {
            var product = new Product("Test name", "Test", "Test", "Test url", Guid.NewGuid(), "Test code", "test user");

            product.UpdateSpecification("new test specification");

            Assert.Equal("new test specification", product.Specification);
        }

        [Fact]
        public void Product_UpdateDescription_DescriptionIsUpdated()
        {
            var product = new Product("Test name", "Test", "Test", "Test url", Guid.NewGuid(), "Test code", "test user");

            product.UpdateDescription("new test description");

            Assert.Equal("new test description", product.Description);
        }
        
        [Fact]
        public void Product_UpdateImage_ImageIsUpdated()
        {
            var product = new Product("Test name", "Test", "Test", "Test url", Guid.NewGuid(), "Test code", "test user");

            product.UpdateImage("new url");

            Assert.Equal("new url", product.Image);
        }

        [Fact]
        public void Product_MarkAsIsDeleted_IsDeletedChangedToTrue()
        {
            var product = new Product("Test name", "Test", "Test", "Test url", Guid.NewGuid(), "Test code", "test user");

            product.MarkAsDeleted();

            Assert.True(product.IsDeleted);
        }

        [Fact]
        public void Product_MarkAsNotIsDeleted_IsDeletedChangedToFalse()
        {
            var product = new Product("Test name", "Test", "Test", "Test url", Guid.NewGuid(), "Test code", "test user");

            product.MarkAsDeleted();
            product.MarkAsNotDeleted();

            Assert.False(product.IsDeleted);
        }

        [Fact]
        public void Product_UpdateProducerCode_ProducerCodeIsUpdated()
        {
            var product = new Product("Test name", "Test", "Test", "Test url", Guid.NewGuid(), "Test code", "test user");

            product.UpdateProducerCode("new producer code");

            Assert.Equal("new producer code", product.ProducerCode);
        }
    }
}
