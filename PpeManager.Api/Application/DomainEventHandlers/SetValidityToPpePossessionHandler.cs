using PpeManager.Domain.Events;

namespace PpeManager.Api.Application.DomainEventHandlers
{
    public class SetValidityToPpePossessionHandler : INotificationHandler<SetValidityToPpePossession>
    {
        private readonly IPpeRepository _ppeRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly NotificationContext _notificationContext;

        public SetValidityToPpePossessionHandler(IPpeRepository ppeRepository, IWorkerRepository workerRepository, NotificationContext notificationContext)
        {
            _ppeRepository = ppeRepository;
            _workerRepository = workerRepository;
            _notificationContext = notificationContext;
        }

        public Task Handle(SetValidityToPpePossession notification, CancellationToken cancellationToken)
        {
            var ppeCertification = _ppeRepository.Find(p => p.PpeCertifications.Select(c => c.Id == notification.PpeCertificationId).FirstOrDefault()).PpeCertifications.Where(p => p.Id == notification.PpeCertificationId).FirstOrDefault(); 
            DateOnly validity;


            if (ppeCertification.Validity.ToDateTime(TimeOnly.MinValue) < DateTime.Now.AddDays(ppeCertification.Durability))
            {
                validity = ppeCertification.Validity;               
            }
            else
            {
                validity= DateOnly.FromDateTime(DateTime.Now.AddDays(ppeCertification.Durability));
            }

            var worker = _workerRepository.Find(w => w.PpePossessions.Select(p => p.Id == notification.PpePossessionId).FirstOrDefault());
            var ppePossession = worker.PpePossessions.Where(p => p.Id == notification.PpePossessionId).FirstOrDefault()?? throw new ArgumentNullException();
            worker.PpePossessions.Remove(ppePossession);
            ppePossession.setValidity(validity);
            worker.PpePossessions.Add(ppePossession);
                        
            _notificationContext.AddNotifications(ppePossession.Notifications);

            if (!_notificationContext.IsValid)
            {
                throw new WorkerDomainException();
            }

            _workerRepository.Update(worker);

            return Task.CompletedTask;

        }
    }
}
