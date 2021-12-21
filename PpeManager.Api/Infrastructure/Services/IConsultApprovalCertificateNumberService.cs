using PpeManager.Domain.ValueTypes;

namespace PpeManager.Api.Infrastructure.Services
{
    public interface IConsultApprovalCertificateNumberService
    {
        DateOnly ConsultValidity(ApprovalCertificate number);
    }
}