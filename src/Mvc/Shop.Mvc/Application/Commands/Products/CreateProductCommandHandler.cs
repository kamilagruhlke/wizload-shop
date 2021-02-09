using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Mvc.Application.Exceptions;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Products
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly productsClient _productsClient;

        private readonly categoriesClient _categoriesClient;

        public CreateProductCommandHandler(productsClient productsClient, categoriesClient categoriesClient)
        {
            _productsClient = productsClient;
            _categoriesClient = categoriesClient;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var producer = await _productsClient.ProducersGetByIdAsync(request.ProducerId, cancellationToken);
            if (producer is null)
            {
                throw new ProducerNotFoundValidationException(request.ProducerId);
            }

            var categories = await _categoriesClient.CategoriesGetActiveCategoriesAsync(cancellationToken);
            if (categories.All(e => e.Id != request.CategoryId))
            {
                throw new CategoryNotFoundValidationException(request.CategoryId);
            }

            await _productsClient.ProductsPostAsync(request.Name,
                request.Description,
                request.Specification,
                request.Image,
                request.NetPrice,
                request.Tax,
                request.ProducerId,
                request.ProducerCode,
                request.CategoryId,
                cancellationToken);

            return true;
        }
    }
}
