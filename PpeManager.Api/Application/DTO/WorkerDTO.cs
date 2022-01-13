namespace PpeManager.Api.Application.DTO
{
    public record WorkerDTO
    {
        public WorkerDTO(int id, string name, string role, string cpf, string registrationNumber, string admissionDate, CompanyDTO? company, int? companyId, IList<PpePossessionDTO>? ppePossessions = null)
        {
            Id = id;
            Name = name;
            Role = role;
            Cpf = cpf;
            RegistrationNumber = registrationNumber;
            AdmissionDate = admissionDate;
            Company = company;
            PpePossessions = ppePossessions;
            CompanyId = companyId;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Role { get; private set; }
        public string Cpf { get; private set; }
        public string RegistrationNumber { get; private set; }
        public string AdmissionDate { get; private set; }
        public CompanyDTO? Company { get; private set; }
        public int? CompanyId { get; set; }
        public IList<PpePossessionDTO>? PpePossessions { get; private set; }


        public static WorkerDTO FromEntity(Worker worker)
        {
            return new WorkerDTO(
               worker.Id,
               worker.Name.ToString(),
               worker.Role,
               worker.Cpf.ToString(),
               worker.RegistrationNumber,
               worker.AdmissionDate.ToString(new CultureInfo("pt-BR")),
               worker.Company is null ? null : CompanyDTO.FromEntity(worker.Company),
               worker.CompanyId,
               worker.PpePossessions?.Select(p => PpePossessionDTO.FromEntity(p)).ToList()
               );
        }

        public bool Equal(WorkerDTO dto) =>
            Id == dto.Id 
            && Name == dto.Name 
            && Role == dto.Role 
            && RegistrationNumber == dto.RegistrationNumber 
            && AdmissionDate == dto.AdmissionDate 
            && dto.Company.Equals(Company) 
            && PpePossessions is null ? dto.PpePossessions is null : dto.PpePossessions is not null && PpePossessions!.SequenceEqual(dto.PpePossessions);
    }
}
