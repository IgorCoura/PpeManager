﻿using PpeManager.Domain.AggregatesModel.AggregateCompany;

namespace PpeManager.Api.Application.Commands.CreateWorkerCommand
{
    public class CreateWorkerCommandHandler : IRequestHandler<CreateWorkerCommand, WorkerDTO>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IWorkerRepository _workerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPpeRepository _ppeRepository;


        public CreateWorkerCommandHandler(NotificationContext notificationContext, IWorkerRepository workerRepository, ICompanyRepository companyRepository, IPpeRepository ppeRepository)
        {
            _notificationContext = notificationContext;
            _workerRepository = workerRepository;
            _companyRepository = companyRepository;
            _ppeRepository = ppeRepository;
        }

        public async Task<WorkerDTO> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var company = _companyRepository.Find(x => x.Id == request.CompanyId);
            var entity = new Worker(request.Name, request.Role, request.Cpf, request.RegistrationNumber, request.AdmissionDate, company);

            if(request.Ppes != null)
            {
                foreach (var p in request.Ppes)
                {
                    var ppe = _ppeRepository.FindById(p.PpeId);
                    entity.AddPpe(ppe);
                }                
            }

            _notificationContext.AddNotifications(entity.Notifications);
            if (!_notificationContext.IsValid)
            {
                throw new WorkerDomainException();
            }


            var entityResult = _workerRepository.Add(entity);

            await _workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return WorkerDTO.FromEntity(entityResult);
        }
    }

    public class CreateWorkerIdentifiedCommandHandler : IdentifiedCommandHandler<CreateWorkerCommand, WorkerDTO>
    {
        public CreateWorkerIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }
    }
}
