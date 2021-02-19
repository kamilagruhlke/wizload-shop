namespace Orders.Domain.Exceptions
{
    public class UnknownDictionaryKeyBusinessException : BusinessException
    {
        public UnknownDictionaryKeyBusinessException(string message) : base(message)
        {
        }
    }
}
