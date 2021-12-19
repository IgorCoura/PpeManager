namespace PpeManager.Domain.Exceptions
{
    public class PpeDomainException: Exception
    {
        public PpeDomainException() { }

        public PpeDomainException(string message): base(message) { }

        public PpeDomainException(string message, Exception innerException): base(message, innerException) { }
    }
}
