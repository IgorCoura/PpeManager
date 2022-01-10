namespace PpeManager.Domain.ValueTypes
{
    public struct Cnpj
    {
        private readonly string _value;
        public readonly Contract<Notification> contract;

        private Cnpj(string value)
        {
            _value = value;
            contract = new Contract<Notification>();
            Validate();
        }

        public override string ToString() =>
            _value;

        public static implicit operator Cnpj(string input) =>
            new Cnpj(input);

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(_value))
            {
                AddNotification("Is necessary to inform the CNPJ.");
                return;
            }

            int[] multiplierOne = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplierTwo = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            List<string> cpfInvalid = new List<string> { "00000000000000", "11111111111111", "22222222222222", "33333333333333", "44444444444444", "55555555555555", "66666666666666", "77777777777777", "88888888888888", "99999999999999" };
            string aux;
            string digit;
            int sum, rest;

            var value = _value.Trim();
            value = value.Replace(".", "").Replace("-", "").Replace("/", "");

            if (value.Length != 14)
            {
                AddNotification("CNPJ should have 14 chars.");
                return;
            }

            if (cpfInvalid.Contains(_value))
            {
                AddNotification("This CNPJ is invalid.");
                return;

            }

            aux = value.Substring(0, 12);
            sum = 0;

            for (int i = 0; i < 12; i++)
                sum += int.Parse(aux[i].ToString()) * multiplierOne[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();
            aux = aux + digit;
            sum = 0;

            for (int i = 0; i < 13; i++)
                sum += int.Parse(aux[i].ToString()) * multiplierTwo[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();

            if (!value.EndsWith(digit))
                AddNotification("This CNPJ is invalid.");
        }

        private void AddNotification(string message)
        {
            contract.AddNotification(nameof(Cnpj), message);
        }
    }
}
