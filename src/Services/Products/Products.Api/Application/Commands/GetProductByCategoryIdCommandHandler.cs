using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Api.Application.Models;
using Products.Domain.AggregateModel.ProductAggregate;

namespace Products.Api.Application.Commands
{
    public class GetProductByCategoryIdCommandHandler : IRequestHandler<GetProductByCategoryIdCommand, IList<ProductModel>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByCategoryIdCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IList<ProductModel>> Handle(GetProductByCategoryIdCommand request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.FindByCategoryId(request.CategoryId, cancellationToken)
                .ConfigureAwait(false);

            return products.Select(e => new ProductModel {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Image = e.Image,
                ProducerCode = e.ProducerCode,
                ProducerId = e.ProducerId,
                Specification = e.Specification,
                CategoryId = e.CategoryId,
                GrossPrice = e.GrossPrice,
                Tax = e.Tax,
                NetPrice = e.NetPrice()
            }).ToList();
        }
    }
}
