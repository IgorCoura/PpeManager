namespace PpeManager.Domain.AggregatesModel.AggregateWorker
{
    public class Worker: Entity, IAggregateRoot
    {
        public Worker(Name name, string role, string registrationNumber, DateOnly admissionDate,Company company, IList<Ppe>? ppes = null, IList<PpePossession>? ppePossessions = null)
        {
            AddNotifications(
                name.contract,
                ValidateRole(role),
                ValidateRegistrationNumber(registrationNumber),
                ValidateAdmissionDate(admissionDate)
                );

            if (IsValid)
            {
                Name = name;
                Role = role;
                RegistrationNumber = registrationNumber;
                AdmissionDate = admissionDate;
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
        public string RegistrationNumber { get; private set; }
        public DateOnly AdmissionDate { get; private set; }
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


        private Contract<Notification> ValidateRole(string role) =>
            new Contract<Notification>()
                .IsNotNullOrEmpty(role, nameof(role), "Role not be null")
                .IsLowerThan(0, role.Length, nameof(role), "Role must have more than one char");

        private Contract<Notification> ValidateRegistrationNumber(string registrationNumber) =>
            new Contract<Notification>()
                .IsNotNullOrEmpty(registrationNumber, nameof(registrationNumber), "Registration not be null")
                .IsLowerThan(0, registrationNumber.Length, nameof(registrationNumber), "Registration Number must have more than one char");

        private Contract<Notification> ValidateAdmissionDate(DateOnly admissionDate) =>
            new Contract<Notification>()
                .IsNotNull(admissionDate, nameof(admissionDate), "Admssion Date not be null")
                .IsLowerThan(DateTime.Now.AddYears(-100), admissionDate.ToDateTime(TimeOnly.MinValue), nameof(admissionDate), "Admission Date has an invalid date")
                .IsGreaterThan(DateTime.Now.AddYears(100), admissionDate.ToDateTime(TimeOnly.MinValue), nameof(admissionDate), "Admission Date has an invalid date");

    }
}
