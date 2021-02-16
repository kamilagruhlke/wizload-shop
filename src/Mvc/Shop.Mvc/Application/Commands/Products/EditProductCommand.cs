using MediatR;
using System;

namespace Shop.Mvc.Application.Commands.Products
{
    public class EditProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

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
