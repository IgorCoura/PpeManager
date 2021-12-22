namespace PpeManager.Api.Application.Commands.CreateCompanyCommand
{
    public class CreateCompanyCommand: IRequest<CompanyDTO>
    {
        public CreateCompanyCommand(string name, string cnpj)
        {
            Name = name;
            Cnpj = cnpj;
        }

        public string Name { get; private set; }
        public string Cnpj { get; private set; }

    }
}
