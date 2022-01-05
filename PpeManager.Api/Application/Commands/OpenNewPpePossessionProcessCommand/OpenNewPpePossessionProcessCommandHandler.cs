namespace PpeManager.Api.Application.Commands.OpenNewPpePossessionProcessCommand
{
    public class OpenNewPpePossessionProcessCommandHandler : IRequestHandler<OpenNewPpePossessionProcessCommand, WorkerDTO>
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IPpeRepository _ppeRepository;

        public OpenNewPpePossessionProcessCommandHandler(IWorkerRepository workerRepository, IPpeRepository ppeRepository)
        {
            _workerRepository = workerRepository;
            _ppeRepository = ppeRepository;
        }

        public async Task<WorkerDTO> Handle(OpenNewPpePossessionProcessCommand request, CancellationToken cancellationToken)
        {
            var worker = _workerRepository.Find(p => p.Id == request.WorkerId);
            var date = DateOnly.FromDateTime(DateTime.Now);

            if (worker.IsOpenPpePossessionProcess)
            {
                throw new PpePossessionProcessException("It is not possible to open a new process with an already open one.");
            }
            else
            {
                worker.setIsOpenPpePossessionProcess(true);
            }

            foreach(var c in request.Certifications)
            {
                var certification = _ppeRepository.Find(p => p.PpeCertifications.Select(x => x.Id == c.ppeCertificationId).FirstOrDefault()).PpeCertifications.Where(x => x.Id == c.ppeCertificationId).FirstOrDefault();
                if (certification! == null!) continue;
                var possession = new PpePossession(worker, certification, date, c.quantity);
                worker.addPpePossession(possession);
            }

            _workerRepository.Update(worker);

            await _workerRepository.UnitOfWork.SaveEntitiesAsync();

            return WorkerDTO.FromEntity(worker);
        }
    }
}
