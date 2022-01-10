namespace PpeManager.Api.Application.Commands.CreateWorkerCommand
{
    public class CreateWorkerCommand : IRequest<WorkerDTO>
    {
        public CreateWorkerCommand(string name, string role, string cpf, string registrationNumber, string admissionDate, int companyId, List<ppe>? ppes)
        {
            Name = name;
            Role = role;
            Cpf = cpf;
            RegistrationNumber = registrationNumber;
            AdmissionDate = admissionDate;
            CompanyId = companyId;
            Ppes = ppes;
        }

        public string Name { get; set; }
        public string Role { get; set; }
        public string Cpf { get; set; }
        public string RegistrationNumber { get; set; }
        public string AdmissionDate { get; set; }
        public int CompanyId { get; set; }
        public List<ppe>? Ppes { get; set; }
    }

    public record ppe
    {
        public int PpeId { get; set; }

        public ppe(int ppeId)
        {
            PpeId = ppeId;
        }
    }
}
