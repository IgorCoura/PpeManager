namespace PpeManager.Domain.ValueTypes
{
    public struct Description
    {
        private readonly string _value;
        public readonly Contract<Notification> contract;

        private Description(string value)
        {
            _value = value;
            contract = new Contract<Notification>();
            Validate();
        }

        public override string ToString() =>
            _value;

        public static implicit operator Description(string value) =>
            new Description(value);

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(_value))
            {
                AddNotification("Inform a valid description.");
                return;
            }

            if (_value.Length < 5)
            {
                AddNotification("The description must have more than 5 chars.");
                return;
            }

        }

        private void AddNotification(string message)
        {
            contract.AddNotification(nameof(Description), message);
        }

    }
}
