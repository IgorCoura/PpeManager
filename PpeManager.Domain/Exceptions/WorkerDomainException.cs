namespace PpeManager.Domain.Exceptions
{
    public class WorkerDomainException: Exception
    {
        public WorkerDomainException() { }

        public WorkerDomainException(string message): base(message) { }

        public WorkerDomainException(string message, Exception innerException): base(message, innerException) { }
    }
}
