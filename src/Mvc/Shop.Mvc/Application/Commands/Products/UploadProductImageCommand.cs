using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Shop.Mvc.Application.Commands.Products
{
    public class UploadProductImageCommand : INotification
    {
        public Guid ProductId { get; set; }

        public IList<IFormFile> Files { get; set; }
    }
}
