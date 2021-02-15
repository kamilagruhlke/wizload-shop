using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Images.Api.Application.Commands
{
    public class UploadProductImageCommand : IRequest<bool>
    {
        public IFormFile File { get; set; }

        public Guid ProductId { get; set; }
    }
}
