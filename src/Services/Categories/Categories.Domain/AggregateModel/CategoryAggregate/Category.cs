using Categories.Domain.SeedWork;
using System;

namespace Categories.Domain.AggregateModel.CategoryAggregate
{
    public class Category : IAggregateRoot
    {
        public Guid Id { get; protected set; }

        public Guid? ParentId { get; protected set; }

        public string Name { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public string CreatedBy { get; protected set; }

        public DateTime UpdatedAt { get; protected set; }

        public string UpdatedBy { get; protected set; }

        public Category(string name, string user) : this(null, name, user)
        {
        }

        public Category(Guid? parentId, string name, string user)
        {
            Id = Guid.NewGuid();
            ParentId = parentId;
            Name = name;
            IsDeleted = false;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = user;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = user;
        }

        protected Category()
        {
        }

        public void UpdateParent(Guid? parentId)
        {
            if (parentId.HasValue)
            {
                ParentId = parentId.Value;
            }
        }

        public void MarkAsDeleted() => IsDeleted = true;

        public void MarkAsNotDeleted() => IsDeleted = false;

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateModificationDates(string user)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = user;
        }
    }
}
