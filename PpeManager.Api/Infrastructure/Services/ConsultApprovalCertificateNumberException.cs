namespace PpeManager.Api.Infrastructure.Services
{
    public class ConsultApprovalCertificateNumberException: Exception
    {
        public ConsultApprovalCertificateNumberException() { }

        public ConsultApprovalCertificateNumberException(string message) : base(message) { }

        public ConsultApprovalCertificateNumberException(string message, Exception innerException) : base(message, innerException) { }
    }
}
