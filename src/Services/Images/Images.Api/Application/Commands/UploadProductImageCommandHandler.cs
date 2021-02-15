using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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

            var base64EncodedBytes = Convert.FromBase64String(request.FileBody);
            var stream = new MemoryStream(base64EncodedBytes);

            var blobClient = container.GetBlobClient(request.FileName);

            await blobClient.UploadAsync(stream, new BlobHttpHeaders { 
                ContentType = MimeMapping.MimeUtility.GetMimeMapping(request.FileName)
            }, cancellationToken: cancellationToken);

            return true;
        }
    }
}
