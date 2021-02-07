using System;
using MediatR;

namespace Shop.Mvc.Application.Commands.Products
{
    public class CreateProductCommand : IRequest<bool>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public string Image { get; set; }

        public double NetPrice { get; set; }

        public double Tax { get; set; }

        public Guid ProducerId { get; set; }

        public string ProducerCode { get; set; }

        public Guid CategoryId { get; set; }
    }
}
