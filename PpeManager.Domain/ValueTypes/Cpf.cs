namespace PpeManager.Domain.ValueTypes
{
    public record Cpf
    {
        private readonly string _value;
        public readonly Contract<Notification> contract;

        private Cpf(string value)
        {
            _value = value;
            contract = new Contract<Notification>();
            Validate();
        }

        public override string ToString() =>
            _value;

        public static implicit operator Cpf(string input) =>
            new Cpf(input);

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(_value))
            {
                AddNotification( "Is necessary to inform the CPF.");
                return;
            }

            int[] multiplierOne = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplierTwo = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            List<string> cpfInvalid = new List<string> { "00000000000", "11111111111", "22222222222", "33333333333", "44444444444", "55555555555", "66666666666", "77777777777", "88888888888", "99999999999" };
            string aux;
            string digit;
            int sum, rest;

            var value = _value.Trim();
            value = _value.Replace(".", "").Replace("-", "");

            if (value.Length != 11)
            {
                AddNotification( "CPF should have 11 chars.");
                return;
            }

            if (cpfInvalid.Contains(_value)) { 
               AddNotification( "This CPF is invalid.");
               return;
                
            }

            aux = value.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(aux[i].ToString()) * multiplierOne[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();
            aux = aux + digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(aux[i].ToString()) * multiplierTwo[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();

            if (!value.EndsWith(digit))
                AddNotification("This CPF is invalid.");
        }

        private void AddNotification( string message)
        {
            contract.AddNotification(nameof(Cpf), message);
        }
    }
}
