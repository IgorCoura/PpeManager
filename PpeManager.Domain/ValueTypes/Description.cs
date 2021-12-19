using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                AddNotification("Inform a valid name.");

            if (_value.Length < 100)
                AddNotification("The name must have more than 10 chars.");

            if (!Regex.IsMatch(_value, (@"[^a-zA-Z0-9]")))
                AddNotification("The name must not have any special char.");
        }

        private void AddNotification(string message)
        {
            contract.AddNotification(nameof(Description), message);
        }

    }
}
