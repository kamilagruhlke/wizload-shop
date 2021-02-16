using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Products
{
    public class UploadProductImageCommandHandler : INotificationHandler<UploadProductImageCommand>
    {
        private readonly imagesClient _imagesClient;

        public UploadProductImageCommandHandler(imagesClient imagesClient) => _imagesClient = imagesClient;

        public async Task Handle(UploadProductImageCommand notification, CancellationToken cancellationToken)
        {
            foreach(var file in notification.Files)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                var fileBody = Convert.ToBase64String(ReadFully(file.OpenReadStream()));

                await _imagesClient.UploadAsync(notification.ProductId, fileName, fileBody)
                    .ConfigureAwait(false);
            }
        }

        private static byte[] ReadFully(Stream input)
        {
            using var memoryStream = new MemoryStream();

            input.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
