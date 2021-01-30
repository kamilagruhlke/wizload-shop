using System;
using Products.Domain.SeedWork;

namespace Products.Domain.AggregateModel.ProductAggregate
{
    public class Product : IAggregateRoot
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public string Specification { get; protected set; }

        public string Image { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public Guid ProducerId { get; protected set; }

        public string ProducerCode { get; protected set; }

        public decimal NetPrice { get; protected set; }

        public decimal Tax { get; protected set; }

        public Guid CategoryId { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public string CreatedBy { get; protected set; }

        public DateTime UpdatedAt { get; protected set; }

        public string UpdatedBy { get; protected set; }

        public Product(string name,
            string description,
            string specification,
            string image,
            Guid producerId,
            string producerCode,
            Guid categoryId,
            decimal netPrice,
            decimal tax,
            string user)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Specification = specification;
            Image = image;
            IsDeleted = false;
            ProducerId = producerId;
            ProducerCode = producerCode;
            CategoryId = categoryId;
            NetPrice = netPrice;
            Tax = tax;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = user;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = user;
        }

        protected Product()
        {
        }

        public decimal GrossPrice() => NetPrice * (1.0m + (Tax/100.0m));

        public void UpdateNetPrice(decimal netPrice) => NetPrice = netPrice;

        public void UpdateTax(decimal tax) => Tax = tax;

        public void UpdateName(string name) => Name = name;

        public void UpdateSpecification(string specification) => Specification = specification;

        public void UpdateDescription(string description) => Description = description;

        public void UpdateImage(string image) => Image = image;

        public void MarkAsDeleted() => IsDeleted = true;

        public void MarkAsNotDeleted() => IsDeleted = false;

        public void UpdateProducerCode(string producerCode) => ProducerCode = producerCode;

        public void UpdateProducerId(Guid producerId) => ProducerId = producerId;

        public void UpdateCategoryId(Guid categoryId) => CategoryId = categoryId;

        public void UpdateModificationDates(string user)
        {
            UpdatedAt = DateTime.Now;
            UpdatedBy = user;
        }
    }
}