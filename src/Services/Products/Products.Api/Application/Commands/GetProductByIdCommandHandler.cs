using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Api.Application.Models;
using Products.Domain.AggregateModel.ProductAggregate;

namespace Products.Api.Application.Commands
{
    public class GetProductByIdCommandHandler : IRequestHandler<GetProductByIdCommand, ProductModel>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductModel> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindById(request.Id, cancellationToken)
                .ConfigureAwait(false);

            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                ProducerCode = product.ProducerCode,
                ProducerId = product.ProducerId,
                Specification = product.Specification,
                CategoryId = product.CategoryId,
                NetPrice = product.NetPrice,
                Tax = product.Tax,
                GrossPrice = product.GrossPrice()
            };
        }
    }
}
