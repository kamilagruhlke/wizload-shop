namespace Products.Domain.Exceptions
{
    public class EntityNotFoundBusinessException : BusinessException
    {
        public EntityNotFoundBusinessException(string message) : base(message)
        {
        }
    }
}
