using Products.Domain.SeedWork;
using System;

namespace Products.Domain.AggregateModel.ProducerAggregate
{
    public class Producer : IAggregateRoot
    {
        public Guid IdProducer { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public string CreatedBy { get; protected set; }

        public DateTime UpdatedAt { get; protected set; }

        public string UpdatedBy { get; protected set; }

        public Producer(string name, string description, string user)
        {
            IdProducer = Guid.NewGuid();
            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = user;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = user;
        }

        protected Producer()
        {

        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void UpdateModificationDates(string user)
        {
            UpdatedAt = DateTime.Now;
            UpdatedBy = user;
        }
    }
}
