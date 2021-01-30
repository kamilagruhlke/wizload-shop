using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Products.Domain.AggregateModel.ProductAggregate;
using Products.Domain.Exceptions;
using Products.Domain.Utils.Interfaces;

namespace Products.Api.Application.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        private readonly IUserAccessor _userAccessor;

        public UpdateProductCommandHandler(IProductRepository productRepository, IUserAccessor userAccessor)
        {
            _productRepository = productRepository;
            _userAccessor = userAccessor;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindById(request.Id, cancellationToken)
                            .ConfigureAwait(false);

            if (product is null)
            {
                throw new EntityNotFoundBusinessException($"Product '{product.Id}' not found");
            }

            product.UpdateName(request.Name);
            product.UpdateDescription(request.Description);
            product.UpdateImage(request.Image);
            product.UpdateProducerId(request.ProducerId);
            product.UpdateProducerCode(request.ProducerCode);
            product.UpdateSpecification(request.Specification);
            product.UpdateCategoryId(request.CategoryId);
            product.UpdateNetPrice(request.NetPrice);
            product.UpdateTax(request.Tax);
            product.UpdateModificationDates(_userAccessor.GetCurrentUsername());

            _productRepository.Update(product);

            return await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
