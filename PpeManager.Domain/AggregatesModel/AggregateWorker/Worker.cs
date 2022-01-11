using System.Globalization;

namespace PpeManager.Domain.AggregatesModel.AggregateWorker
{
    public class Worker : Entity, IAggregateRoot
    {
        public Worker(Name name, string role, Cpf cpf, string registrationNumber, string admissionDate, Company company, IList<Ppe>? ppes = null, IList<PpePossession>? ppePossessions = null)
        {   
            AddNotifications(
                cpf.contract,
                name.contract,
                ValidateRole(role),
                ValidateRegistrationNumber(registrationNumber),
                ValidateAdmissionDate(admissionDate)
                );

 
                Cpf = cpf;
                Name = name;
                Role = role;
                RegistrationNumber = registrationNumber;
                AdmissionDate = DateOnly.Parse(admissionDate, new CultureInfo("pt-BR"), DateTimeStyles.None);
                Company = company;
                Ppes = ppes ?? new List<Ppe>();
                PpePossessions = ppePossessions ?? new List<PpePossession>();

        }       

        public Worker() { }
 

        public Name Name { get; private set; }
        public string Role { get; private set; }
        public Cpf Cpf { get; private set; }
        public string RegistrationNumber { get; private set; }
        public DateOnly AdmissionDate { get; private set; }
        public bool IsOpenPpePossessionProcess { get; private set; } = false;
        public void setIsOpenPpePossessionProcess(bool var)
        {
            IsOpenPpePossessionProcess = var;
        }
        public virtual Company Company { get; private set; }
        public int CompanyId { get; private set; }
        public virtual IList<Ppe> Ppes { get; private set; }
        public virtual IList<PpePossession> PpePossessions { get; private set; }

        public DateOnly? DueDate { get; private set; }
        public int PpesNotDelivered { get; private set; } = 0;


        public void setCompany(Company company)
        {
            AddNotifications(
                company.Notifications
                );

            Company = company; 
        }

        public void AddPpe(Ppe value)
        {
            AddNotifications(value.Notifications);
            Ppes.Add(value);
            PpesNotDelivered++;
        }
        public void addPpePossession(PpePossession ppe)
        {
            AddNotifications(ppe.Notifications);

            if (Ppes.Where(p => p.Id == ppe.PpeCertification!.PpeId).FirstOrDefault() is null)
            {
                AddNotification(new Notification(nameof(Worker),$"The worker {Name} is not allowed to receive this ppe."));
                return;
            }

            var result = PpePossessions.Count <= 0 ? default: PpePossessions.Where(p => p.PpeCertification!.PpeId == ppe.PpeCertification!.PpeId).FirstOrDefault();

            if(result is null)
            {   
                PpesNotDelivered--;
            }

            PpePossessions.Add(ppe);

            if (DueDate > ppe.Validity || DueDate is null)
            {
                DueDate = ppe.Validity;
            }

        }


        private Contract<Notification> ValidateRole(string role) =>
            new Contract<Notification>()
                .IsNotNullOrEmpty(role, nameof(role), "Role not be null")
                .IsLowerThan(0, role.Length, nameof(role), "Role must have more than one char");

        private Contract<Notification> ValidateRegistrationNumber(string registrationNumber) =>
            new Contract<Notification>()
                .IsNotNullOrEmpty(registrationNumber, nameof(registrationNumber), "Registration not be null")
                .IsLowerThan(0, registrationNumber.Length, nameof(registrationNumber), "Registration Number must have more than one char");

        private Contract<Notification> ValidateAdmissionDate(string admissionDateString)
        {
            var contract = new Contract<Notification>();
            if (DateOnly.TryParse(admissionDateString, new CultureInfo("pt-BR"), DateTimeStyles.None, out var admissionDate))
            {
                contract
                .IsNotNull(admissionDate, nameof(admissionDate), "Admssion Date not be null")
                .IsLowerThan(DateTime.Now.AddYears(-100), admissionDate.ToDateTime(TimeOnly.MinValue), nameof(admissionDate), "Admission Date has an invalid date")
                .IsGreaterThan(DateTime.Now.AddYears(100), admissionDate.ToDateTime(TimeOnly.MinValue), nameof(admissionDate), "Admission Date has an invalid date");
            }
            else
            {
                contract.AddNotification(new Notification(nameof(admissionDate), "it was not possible to convert string to date"));
            }
            return contract;
        }
    }
}
