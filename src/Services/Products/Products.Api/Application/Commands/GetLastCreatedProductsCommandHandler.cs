using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Api.Application.Models;
using Products.Domain.AggregateModel.ProductAggregate;

namespace Products.Api.Application.Commands
{
    public class GetLastCreatedProductsCommandHandler : IRequestHandler<GetLastCreatedProductsCommand, IList<ProductModel>>
    {
        private readonly IProductRepository _productRepository;

        public GetLastCreatedProductsCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IList<ProductModel>> Handle(GetLastCreatedProductsCommand request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetLast(request.NumberOfItems, cancellationToken)
                .ConfigureAwait(false);

            return products.Select(e => new ProductModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Image = e.Image,
                ProducerCode = e.ProducerCode,
                ProducerId = e.ProducerId,
                Specification = e.Specification,
                CategoryId = e.CategoryId,
                NetPrice = e.NetPrice,
                Tax = e.Tax,
                GrossPrice = e.GrossPrice()
            }).ToList();
        }
    }
}
