using System.Threading;
using System.Threading.Tasks;
using Categories.Domain.AggregateModel.CategoryAggregate;
using Categories.Domain.Utils.Interfaces;
using MediatR;

namespace Categories.Api.Application.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IUserAccessor _userAccessor;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUserAccessor userAccessor)
        {
            _categoryRepository = categoryRepository;
            _userAccessor = userAccessor;
        }

        public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(request.ParentId, request.Name, _userAccessor.GetCurrentUsername());

            await _categoryRepository.Add(category, cancellationToken)
                .ConfigureAwait(false);

            return await _categoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
