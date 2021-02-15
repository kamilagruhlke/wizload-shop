using System;
using System.Collections.Generic;
using Images.Api.Application.Models;
using MediatR;

namespace Images.Api.Application.Commands
{
    public class GetImagesByProductIdsCommand : IRequest<List<ProductImageModel>>
    {
        public List<Guid> ProductIds { get; set; }
    }
}
