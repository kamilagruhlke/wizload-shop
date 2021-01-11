using Products.Domain.SeedWork;
using System;

namespace Products.Domain.AggregateModel.ProductAggregate
{
    class Product : IAggregateRoot
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public string Specification { get; protected set; }

        public string Image { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public Guid ProducerId { get; protected set; }

        public string ProducerCode { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public string CreatedBy { get; protected set; }

        public DateTime UpdatedAt { get; protected set; }

        public string UpdatedBy { get; protected set; }

        public Product(string name, string description, string specification, string image, Guid producerId, string producerCode, string user)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Specification = specification;
            Image = image;
            IsDeleted = false;
            ProducerId = producerId;
            ProducerCode = producerCode;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = user;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = user;
        }

        protected Product()
        {
        }


        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateSpecification(string specification)
        {
            Specification = specification;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void UpdateImage(string image)
        {
            Image = image;
        }

        public void MarkAsDeleted() => IsDeleted = true;

        public void MarkAsNotDeleted() => IsDeleted = false;

        public void UpdateModificationDates(string user)
        {
            UpdatedAt = DateTime.Now;
            UpdatedBy = user;
        }

        public void UpdateProducerCode(string producerCode)
        {
            ProducerCode = producerCode;
        }
    }
}