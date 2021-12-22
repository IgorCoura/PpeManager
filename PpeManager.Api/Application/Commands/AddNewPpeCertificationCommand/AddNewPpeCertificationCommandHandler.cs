using PpeManager.Api.Infrastructure.Services;

namespace PpeManager.Api.Application.Commands.AddNewPpeCertificationCommand
{
    public class AddNewPpeCertificationCommandHandler : IRequestHandler<AddNewPpeCertificationCommand, PpeDTO>
    {
        private readonly IPpeRepository _ppeRepository;
        private readonly NotificationContext _notificationContext;
        private readonly IConsultApprovalCertificateNumberService _consultApprovalCertificateNumberService;
        public AddNewPpeCertificationCommandHandler(IPpeRepository ppeRepository, NotificationContext notificationContext, IConsultApprovalCertificateNumberService consultApprovalCertificateNumberService)
        {
            _ppeRepository = ppeRepository;
            _notificationContext = notificationContext;
            _consultApprovalCertificateNumberService = consultApprovalCertificateNumberService;
        }

        public Task<PpeDTO> Handle(AddNewPpeCertificationCommand request, CancellationToken cancellationToken)
        {
            

            var validity =  _consultApprovalCertificateNumberService.ConsultValidity(request.ApprovalCertificateNumber);

            var ppeOld = _ppeRepository.Find(ppe => ppe.Id == request.PpeId);
            var ppeCertification = new PpeCertification(ppeOld, request.ApprovalCertificateNumber, validity , request.Durability);
            _notificationContext.AddNotifications(ppeCertification.Notifications);       
            
            if (!_notificationContext.IsValid)
                throw new PpeDomainException();

            ppeOld.addNewPpeCertification(ppeCertification);

            var ppe = _ppeRepository.Update(ppeOld);

            var dto = PpeDTO.FromEntity(ppe);

            return Task.FromResult(dto);
        }

    }

    public class AddNewPpeCertificationIdentifiedCommandHandler : IdentifiedCommandHandler<AddNewPpeCertificationCommand, PpeDTO>
    {
        public AddNewPpeCertificationIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }
    }
}
