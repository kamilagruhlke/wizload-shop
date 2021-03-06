﻿using System;

namespace Products.Api.Application.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public string Image { get; set; }

        public Guid ProducerId { get; set; }

        public string ProducerCode { get; set; }

        public Guid CategoryId { get; set; }

        public decimal NetPrice { get; set; }

        public decimal Tax { get; set; }

        public decimal GrossPrice { get; set; }
    }
}
