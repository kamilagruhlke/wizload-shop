using System;

namespace Shop.Mvc.Application.Exceptions
{
    public class ProducerNotFoundValidationException : ValidationException
    {
        public ProducerNotFoundValidationException(Guid id) : base($"Producer '{id}' not found")
        {
        }
    }
}
