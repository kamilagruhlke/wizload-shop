using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Products
{
    public class GetProductImagesCommandHandler : IRequestHandler<GetProductImagesCommand, List<string>>
    {
        private readonly imagesClient _imagesClient;

        public GetProductImagesCommandHandler(imagesClient imagesClient) => _imagesClient = imagesClient;

        public async Task<List<string>> Handle(GetProductImagesCommand request, CancellationToken cancellationToken)
        {
            var result = await _imagesClient.ImagesAsync(new List<Guid> { request.Id }, cancellationToken);
            return result.FirstOrDefault()?.Urls?.ToList();
        }
    }
}
