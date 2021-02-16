using MediatR;
using System;

namespace Shop.Mvc.Application.Commands.Products
{
    public class GetProductRandomImageCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }
}
