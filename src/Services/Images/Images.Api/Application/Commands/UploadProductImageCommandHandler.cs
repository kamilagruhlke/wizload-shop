using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Images.Api.Application.Commands
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommand, bool>
    {
        private readonly IConfiguration _configuration;

        public UploadProductImageCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
        {
            var blobServiceClient = new BlobServiceClient(_configuration.GetConnectionString("AzureBlobStorage"));

            var container = blobServiceClient.GetBlobContainerClient(request.ProductId.ToString());
            if (await container.ExistsAsync(cancellationToken) == false)
            {
                await container.CreateAsync(cancellationToken: cancellationToken);
            }

            await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

            await container.UploadBlobAsync($"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}",
                request.File.OpenReadStream(),
                cancellationToken);

            return true;
        }
    }
}
