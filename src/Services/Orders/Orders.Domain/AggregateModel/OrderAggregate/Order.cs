using Orders.Domain.Exceptions;
using Orders.Domain.SeedWork;
using Orders.Domain.Utils.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Orders.Domain.AggregateModel.OrderAggregate
{
    public class Order : IAggregateRoot
    {
        public Guid Id { get; protected set; }

        public List<OrderedProduct> OrderedProducts { get; protected set; }

        public string Status { get; protected set; }

        public decimal ValueNet { get; protected set; }

        public decimal ValueTax { get; protected set; }

        public string Address { get; protected set; }

        public string City { get; protected set; }

        public string PostalCode { get; protected set; }

        public string CreatedBy { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public DateTime? UpdatedAt { get; protected set; }

        public string UpdatedBy { get; protected set; }

        public Order(decimal valueNet, decimal valueTax, string addres, string city, string postalCode, string user)
        {
            Id = Guid.NewGuid();
            OrderedProducts = new List<OrderedProduct>();
            ValueNet = valueNet;
            ValueTax = valueTax;
            CreatedAt = DateTime.UtcNow;
            Address = addres;
            City = city;
            PostalCode = postalCode;
            CreatedBy = user;
            UpdatedBy = null;
            UpdatedAt = null;
            Status = StatusDictionary.Pending;
        }

        protected Order()
        {

        }

        public void UpdateStatus(string status, string user)
        {
            if (StatusDictionary.Statuses().Contains(status) == false)
            {
                throw new UnknownDictionaryKeyBusinessException("Not Found");
            }

            Status = status;
            UpdatedBy = user;
            UpdatedAt = DateTime.UtcNow;
        }

        public decimal ValueGross() => ValueNet * (1.0m + (ValueTax / 100.0m));

        public decimal UpdateValueNetto(decimal valueNet) => ValueNet = valueNet;

        public decimal UpdateValueTax(decimal valueTax) => ValueTax = valueTax;

        public void UpdateModificationDates(string user)
        {
            UpdatedAt = DateTime.Now;
            UpdatedBy = user;
        }

        public void UpdateAddProductToList(OrderedProduct orderedProduct)
        {
            OrderedProducts.Add(orderedProduct);
        }
    }
}
