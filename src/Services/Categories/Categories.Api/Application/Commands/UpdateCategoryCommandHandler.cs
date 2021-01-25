using System.Threading;
using System.Threading.Tasks;
using Categories.Domain.AggregateModel.CategoryAggregate;
using Categories.Domain.Exceptions;
using Categories.Domain.Utils.Interfaces;
using MediatR;

namespace Categories.Api.Application.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IUserAccessor _userAccessor;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUserAccessor userAccessor)
        {
            _categoryRepository = categoryRepository;
            _userAccessor = userAccessor;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindById(request.Id, cancellationToken)
                .ConfigureAwait(false);

            if (category is null)
            {
                throw new EntityNotFoundBusinessException($"Category with id '{request.Id}' not found");
            }

            category.UpdateName(request.Name);
            category.UpdateParent(category.ParentId);
            category.UpdateModificationDates(_userAccessor.GetCurrentUsername());

            if (request.IsDeleted)
            {
                category.MarkAsDeleted();
            }
            else
            {
                category.MarkAsNotDeleted();
            }

            _categoryRepository.Update(category);

            return await _categoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
