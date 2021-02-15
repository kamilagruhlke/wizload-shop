using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Images.Api.Application.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Images.Api.Application.Commands
{
    public class GetImagesByProductIdsCommandHandler : IRequestHandler<GetImagesByProductIdsCommand, List<ProductImageModel>>
    {
        private readonly IConfiguration _configuration;

        public GetImagesByProductIdsCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<ProductImageModel>> Handle(GetImagesByProductIdsCommand request, CancellationToken cancellationToken)
        {
            var result = new List<ProductImageModel>();

            var blobServiceClient = new BlobServiceClient(_configuration.GetConnectionString("AzureBlobStorage"));

            foreach (var productId in request.ProductIds)
            {
                var productImageModel = new ProductImageModel
                {
                    ProductId = productId,
                    Urls = new List<string>()
                };

                var container = blobServiceClient.GetBlobContainerClient(productId.ToString());
                if (await container.ExistsAsync(cancellationToken) == false)
                {
                    await container.CreateAsync(cancellationToken: cancellationToken);
                }

                await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

                var resultSegment = container.GetBlobsAsync().AsPages(default, 100);

                await foreach (Azure.Page<BlobItem> blobPage in resultSegment)
                {
                    foreach (BlobItem blobItem in blobPage.Values)
                    {
                        productImageModel.Urls.Add(blobItem.Name);
                    }
                }

                if(productImageModel.Urls.Count > 0)
                {
                    result.Add(productImageModel);
                }
            }

            return result;
        }
    }
}
