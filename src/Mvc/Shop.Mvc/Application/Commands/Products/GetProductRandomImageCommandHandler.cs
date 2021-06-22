using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Products
{
    public class GetProductRandomImageCommandHandler : IRequestHandler<GetProductRandomImageCommand, string>
    {
        private readonly imagesClient _imagesClient;

        public GetProductRandomImageCommandHandler(imagesClient imagesClient) => _imagesClient = imagesClient;

        public async Task<string> Handle(GetProductRandomImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _imagesClient.ImagesAsync(new List<Guid> { request.Id }, cancellationToken);
                var imageUrls = result.FirstOrDefault()?.Urls?.ToList();

                if (imageUrls is null || imageUrls.Count <= 0)
                {
                    return "/img/no-image.png";
                }

                return imageUrls.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            }
            catch
            {
                return "/img/no-image.png";
            }
        }
    }
}
