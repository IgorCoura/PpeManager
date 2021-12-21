namespace PpeManager.Domain.AggregatesModel.AggregateWorker
{
    public class Worker: Entity
    {
        public Worker(Name name, string role, string registrationNumber, DateOnly admissionDate, Company company, IList<Ppe> ppes, IList<PpePossession>? ppePossessions = null)
        {
            AddNotifications(
                name.contract,
                ValidateRole(role),
                ValidateRegistrationNumber(registrationNumber),
                ValidateAdmissionDate(admissionDate),
                (Notifiable<Notification>)company.Notifications
                );

            if (IsValid)
            {
                Name = name;
                Role = role;
                RegistrationNumber = registrationNumber;
                AdmissionDate = admissionDate;
                Company = company;
                Ppes = ppes;
                PpePossessions = ppePossessions ?? new List<PpePossession>();
            }
        }

        public Name Name { get; private set; }
        public string Role { get; private set; }
        public string RegistrationNumber { get; private set; }
        public DateOnly AdmissionDate { get; private set; }
        public Company Company { get; private set; }
        public IList<Ppe> Ppes { get; private set; }
        public IList<PpePossession> PpePossessions { get; private set; }


        private Contract<Notification> ValidateRole(string role) =>
            new Contract<Notification>()
                .IsNotNullOrEmpty(nameof(role), "Role not be null")
                .IsGreaterThan(0, role.Length, nameof(role), "Role must have more than one char");

        private Contract<Notification> ValidateRegistrationNumber(string registrationNumber) =>
            new Contract<Notification>()
                .IsNotNullOrEmpty(nameof(registrationNumber), "Registration not be null")
                .IsGreaterThan(0, registrationNumber.Length, nameof(registrationNumber), "Registration Number must have more than one char");

        private Contract<Notification> ValidateAdmissionDate(DateOnly admissionDate) =>
            new Contract<Notification>()
                .IsNotNullOrEmpty(nameof(admissionDate), "Admssion Date not be null")
                .IsLowerThan(DateTime.Now.AddYears(-100), admissionDate.ToDateTime(TimeOnly.MinValue), nameof(admissionDate), "Admission Date has an invalid date")
                .IsGreaterThan(DateTime.Now.AddYears(100), admissionDate.ToDateTime(TimeOnly.MinValue), nameof(admissionDate), "Admission Date has an invalid date");

    }
}
