namespace PpeManager.Api.Application.Commands.CreateCompanyCommand
{
    public class CreateCompanyCommand : IRequest<CompanyDTO>
    {
        public CreateCompanyCommand(string nickName, string name, string cnpj)
        {
            NickName = nickName;    
            Name = name;
            Cnpj = cnpj;
        }

        public string NickName { get; set; }
        public string Name { get; private set; }
        public string Cnpj { get; private set; }

    }
}
