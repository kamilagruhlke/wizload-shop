using MediatR;
using Shop.Mvc.Application.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Products
{
    public class EditProductCommandHandler : IRequestHandler<EditProductCommand, bool>
    {
        private readonly productsClient _productsClient;

        private readonly categoriesClient _categoriesClient;

        public EditProductCommandHandler(productsClient productsClient, categoriesClient categoriesClient)
        {
            _productsClient = productsClient;
            _categoriesClient = categoriesClient;
        }

        public async Task<bool> Handle(EditProductCommand request, CancellationToken cancellationToken)
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

            await _productsClient.ProductsPutAsync(request.Id,
                request.Name,
                request.Description,
                request.Specification,
                request.Image,
                request.ProducerId,
                request.ProducerCode,
                request.CategoryId,
                request.NetPrice,
                request.Tax,
                cancellationToken);

            return true;
        }
    }
}
