using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Domain.ValueTypes
{
    public struct ApprovalCertificate
    {
        private readonly string _value;
        public readonly Contract<Notification> contract;

        private ApprovalCertificate(string value)
        {
            _value = value;
            contract = new Contract<Notification>();
            Validate();
        }

        public override string ToString() =>
            _value;

        public static implicit operator ApprovalCertificate(string value) =>
            new ApprovalCertificate(value);

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(_value))
            {
                AddNotification("Inform a valid Approval Certificate.");
                return;
            }

            if (_value.Length < 5 || _value.Length > 5)
            {
                AddNotification("The Approval Certificate must have 5 digits.");
                return;
            }

            if (Regex.IsMatch(_value, (@"[^0-9]")))
            {
                AddNotification("The Approval Certificate must have only numbers.");
                return;
            }

        }

        private void AddNotification(string message)
        {
            contract.AddNotification(nameof(ApprovalCertificate), message);
        }
    }
}
