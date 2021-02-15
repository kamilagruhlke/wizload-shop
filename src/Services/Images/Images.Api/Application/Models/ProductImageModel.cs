using System;
using System.Collections.Generic;

namespace Images.Api.Application.Models
{
    public class ProductImageModel
    {
        public Guid ProductId { get; set; }

        public List<string> Urls { get; set; }
    }
}
