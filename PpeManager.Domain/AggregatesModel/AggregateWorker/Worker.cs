namespace PpeManager.Domain.AggregatesModel.AggregateWorker
{
    public class Worker: Entity, IAggregateRoot
    {
        public Worker(Name name, string role, Cpf cpf, string registrationNumber, string admissionDate,Company company, IList<Ppe>? ppes = null, IList<PpePossession>? ppePossessions = null)
        {
            AddNotifications(
                cpf.contract,
                name.contract,
                ValidateRole(role),
                ValidateRegistrationNumber(registrationNumber),
                ValidateAdmissionDate(admissionDate)
                );

            if (IsValid)
            {
                Cpf = cpf;
                Name = name;
                Role = role;
                RegistrationNumber = registrationNumber;
                AdmissionDate = DateOnly.Parse(admissionDate);
                Company = company;
                Ppes = ppes ?? new List<Ppe>();
                PpePossessions = ppePossessions ?? new List<PpePossession>();
            }
        }

        public Worker()
        {

        }

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
        private int _companyId;
        public int getCompanyId => _companyId;
        public virtual IList<Ppe> Ppes { get; private set; }
        public virtual IList<PpePossession> PpePossessions { get; private set; }
        

        public void setCompany(Company company)
        {
            AddNotifications(
                company.Notifications
                );
            if (IsValid)
            {
                Company = company;
            }
            
        }

        public void setPpePossession(List<PpePossession> value)
        {
            PpePossessions = value;
        }

        public void addPpePossession(PpePossession ppe)
        {
            if (ppe.IsValid)
            {
                PpePossessions.Add(ppe);
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
            if (DateOnly.TryParse(admissionDateString, out var admissionDate) ) {
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
