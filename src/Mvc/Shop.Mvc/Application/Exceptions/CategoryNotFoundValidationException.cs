using System;

namespace Shop.Mvc.Application.Exceptions
{
    public class CategoryNotFoundValidationException : ValidationException
    {
        public CategoryNotFoundValidationException(Guid id) : base($"category {id} not found")
        {
        }
    }
}
