using PpeManager.Domain.ValueTypes;

namespace PpeManager.Api.Infrastructure.Services
{
    public interface IConsultApprovalCertificateNumberService
    {
        DateTime ConsultValidity(ApprovalCertificate number);
    }
}