namespace PpeManager.Domain.ValueTypes
{
    public struct Name
    {
        private readonly string _value;
        public readonly Contract<Notification> contract;

        private Name(string value)
        {
            _value = value;
            contract = new Contract<Notification>();
            Validate();
        }

        public override string ToString() =>
            _value;

        public static implicit operator Name(string value) =>
            new Name(value);

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(_value))
            {
                AddNotification("Inform a valid name.");
                return;
            }                

            if (_value.Length < 1)
            {
                AddNotification("The name must have more than 1 chars.");
                return;
            }                

            if (Regex.IsMatch(_value, (@"[^a-zA-Z0-9]")))
            {
                AddNotification("The name must not have any special char.");
                return;
            }               

        }

        private void AddNotification(string message)
        {
            contract.AddNotification(nameof(Name), message);
        }
    }
}
