using System;
using System.Collections.Generic;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }

        public List<ProductModel> OrderedProducts { get; set; }

        public string Status { get; set; }

        public decimal ValueNet { get; set; }

        public decimal ValueTax { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string ClientFullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }
}
