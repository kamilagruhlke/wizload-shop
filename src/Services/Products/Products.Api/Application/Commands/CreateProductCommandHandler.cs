using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Domain.AggregateModel.ProductAggregate;
using Products.Domain.Utils.Interfaces;

namespace Products.Api.Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        private readonly IUserAccessor _userAccessor;

        public CreateProductCommandHandler(IProductRepository productRepository, IUserAccessor userAccessor)
        {
            _productRepository = productRepository;
            _userAccessor = userAccessor;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name,
                request.Description,
                request.Specification,
                request.Image,
                request.ProducerId,
                request.ProducerCode,
                request.CategoryId,
                request.GrossPrice,
                request.Tax,
                _userAccessor.GetCurrentUsername());

            await _productRepository.Add(product, cancellationToken)
                .ConfigureAwait(false);

            return await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
