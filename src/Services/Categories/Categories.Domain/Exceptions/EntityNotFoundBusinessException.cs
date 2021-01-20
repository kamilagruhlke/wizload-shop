namespace Categories.Domain.Exceptions
{
    public class EntityNotFoundBusinessException : BusinessException
    {
        public EntityNotFoundBusinessException(string message) : base(message)
        {
        }
    }
}
