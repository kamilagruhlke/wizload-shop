using MediatR;
using System;

namespace Images.Api.Application.Commands
{
    public class UploadProductImageCommand : IRequest<bool>
    {
        public string FileBody { get; set; }

        public string FileName { get; set; }

        public Guid ProductId { get; set; }
    }
}
