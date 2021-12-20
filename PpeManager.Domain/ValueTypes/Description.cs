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
            {
                AddNotification("Inform a valid description.");
                return;
            }               

            if (_value.Length < 5)
            {
                AddNotification("The description must have more than 5 chars.");
                return;
            }
                
            if (Regex.IsMatch(_value, (@"[^a-zA-Z0-9]")))
            {
                AddNotification("The description must not have any special char.");
                return;
            }
                
        }

        private void AddNotification(string message)
        {
            contract.AddNotification(nameof(Description), message);
        }

    }
}
