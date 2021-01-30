using System;
using MediatR;

namespace Products.Api.Application.Commands
{
    public class CreateProductCommand : IRequest<bool>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public string Image { get; set; }

        public decimal NetPrice { get; set; }

        public decimal Tax { get; set; }

        public Guid ProducerId { get; set; }

        public string ProducerCode { get; set; }

        public Guid CategoryId { get; set; }
    }
}
