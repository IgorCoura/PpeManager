namespace PpeManager.Api.Application.Commands.ClosePpePossessionProcessCommand
{
    public class ClosePpePossessionProcessCommandHandler : IRequestHandler<ClosePpePossessionProcessCommand, WorkerDTO>
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        public ClosePpePossessionProcessCommandHandler(IWorkerRepository workerRepository, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _workerRepository = workerRepository;
            _configuration = configuration;
            _environment = environment;
        }

        public async Task<WorkerDTO> Handle(ClosePpePossessionProcessCommand request, CancellationToken cancellationToken)
        {
            var worker = _workerRepository.Find(x => x.Id == request.WorkerId);
            if (!worker.IsOpenPpePossessionProcess)
            {
                throw new PpePossessionProcessException("it is impossible to close a non-existent process");
            }
            var formFile = request.File;
            if (formFile.Length > 0)
            {
                if (!Directory.Exists(_environment.WebRootPath + "docs"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "docs");
                }
                var filePath = Path.Combine("docs", Guid.NewGuid().ToString() + ".pdf");

                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                    stream.Flush();
                }

                foreach (var p in worker.PpePossessions)
                {
                    if (p.Confirmation == false)
                    {
                        p.confirmation(true, filePath);
                    }
                }

                worker.setIsOpenPpePossessionProcess(false);

                _workerRepository.Update(worker);
            }

            await _workerRepository.UnitOfWork.SaveEntitiesAsync();

            return WorkerDTO.FromEntity(worker);
        }
    }


    public class ClosePpePossessionProcessIdentifiedCommandHandler : IdentifiedCommandHandler<ClosePpePossessionProcessCommand, WorkerDTO>
    {
        public ClosePpePossessionProcessIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }
    }
}
