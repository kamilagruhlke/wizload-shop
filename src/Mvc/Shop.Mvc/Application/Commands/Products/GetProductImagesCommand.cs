using MediatR;
using System;
using System.Collections.Generic;

namespace Shop.Mvc.Application.Commands.Products
{
    public class GetProductImagesCommand : IRequest<List<string>>
    {
        public Guid Id { get; set; }
    }
}
