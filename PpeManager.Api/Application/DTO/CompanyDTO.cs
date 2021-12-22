using PpeManager.Domain.AggregatesModel.AggregateCompany;

namespace PpeManager.Api.Application.DTO
{
    public record CompanyDTO
    {
        public CompanyDTO(int id, string name, string cnpj)
        {
            Id = id;
            Name = name;
            Cnpj = cnpj;
        }


        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Cnpj { get; private set; }

        public static CompanyDTO FromEntity(Company entity)
        {
            return new CompanyDTO(entity.Id, entity.Name.ToString(), entity.Cnpj.ToString());

        }

        public bool Equal(CompanyDTO dto) =>
            dto.Id == Id && dto.Name == Name && dto.Cnpj == Cnpj;
    }
}
