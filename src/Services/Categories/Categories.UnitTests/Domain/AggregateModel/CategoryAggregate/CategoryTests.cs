using Categories.Domain.AggregateModel.CategoryAggregate;
using System;
using Xunit;

namespace Categories.UnitTests.Domain.AggregateModel.CategoryAggregate
{
    public class CategoryTests
    {
        [Fact]
        public void Category_Constructor_CategoryCreatedCorrectly()
        {
            var category = new Category("TEST", "Test user");

            Assert.Equal("TEST", category.Name);
            Assert.Equal("Test user", category.CreatedBy);
            Assert.Equal("Test user", category.UpdatedBy);
            Assert.False(category.IsDeleted);
            Assert.Null(category.ParentId);
            Assert.NotEqual(Guid.Empty, category.Id);
        }

        [Fact]
        public void Category_Constructor_CategoryWithParentCreated()
        {
            var parentId = Guid.NewGuid();
            var category = new Category(parentId, "TEST", "Test user");

            Assert.Equal("TEST", category.Name);
            Assert.Equal("Test user", category.CreatedBy);
            Assert.Equal("Test user", category.UpdatedBy);
            Assert.False(category.IsDeleted);
            Assert.Equal(parentId, category.ParentId);
            Assert.NotEqual(Guid.Empty, category.Id);
        }

        [Fact]
        public void Category_UpdateParent_ParentIdUpdated()
        {
            var category = new Category("TEST", "Test user");
            var parentId = Guid.NewGuid();

            category.UpdateParent(parentId);

            Assert.Equal(parentId, category.ParentId);
        }

        [Fact]
        public void Category_MarkAsDeleted_IsDeletedChangedToTrue()
        {
            var category = new Category("TEST", "Test user");

            category.MarkAsDeleted();

            Assert.True(category.IsDeleted);
        }

        [Fact]
        public void Category_MarkAsNotDeleted_IsDeletedChangedToFalse()
        {
            var category = new Category("TEST", "Test user");

            category.MarkAsDeleted();
            category.MarkAsNotDeleted();

            Assert.False(category.IsDeleted);
        }

        [Fact]
        public void Category_UpdateName_NameIsUpdated()
        {
            var category = new Category("TEST", "Test user");

            category.UpdateName("NEW NAME");

            Assert.Equal("NEW NAME", category.Name);
        }
    }
}
