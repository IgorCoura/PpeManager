namespace PpeManager.Api.Application.DTO
{
    public record WorkerDTO
    {
        public WorkerDTO(int id, string name, string role, string registrationNumber, string admissionDate, CompanyDTO company, IList<PpeWithoutCertificationsDTO>? ppes = null, IList<PpePossessionDTO>? ppePossessions = null)
        {
            Id = id;
            Name = name;
            Role = role;
            RegistrationNumber = registrationNumber;
            AdmissionDate = admissionDate;
            Company = company;
            Ppes = ppes ?? new List<PpeWithoutCertificationsDTO>();
            PpePossessions = ppePossessions ?? new List<PpePossessionDTO>();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Role { get; private set; }
        public string RegistrationNumber { get; private set; }
        public string AdmissionDate { get; private set; }
        public CompanyDTO Company { get; private set; }
        public IList<PpeWithoutCertificationsDTO> Ppes { get; private set; }
        public IList<PpePossessionDTO> PpePossessions { get; private set; }


        public static WorkerDTO FromEntity(Worker worker)
        {
            return new WorkerDTO(worker.Id, worker.Name.ToString(), worker.Role, worker.RegistrationNumber, worker.AdmissionDate.ToString(), CompanyDTO.FromEntity(worker.Company) , worker.Ppes.Select(p => PpeWithoutCertificationsDTO.FromEntity(p)).ToList(), worker.PpePossessions.Select(p => PpePossessionDTO.FromEntity(p)).ToList());
        }

        public bool Equal(WorkerDTO dto) =>
            Id == dto.Id && Name == dto.Name && Role == dto.Role && RegistrationNumber == dto.RegistrationNumber && AdmissionDate == dto.AdmissionDate && dto.Company.Equals(Company) && Ppes.SequenceEqual(dto.Ppes) && PpePossessions.SequenceEqual(dto.PpePossessions);

    }
}
