namespace PpeManager.Api.Application.Exceptions
{
    public class PpePossessionProcessException: Exception
    {
        public PpePossessionProcessException() { }
        public PpePossessionProcessException(string message) : base(message) { }
        public PpePossessionProcessException(string message, Exception innerException ): base(message, innerException) { } 
    }
}
