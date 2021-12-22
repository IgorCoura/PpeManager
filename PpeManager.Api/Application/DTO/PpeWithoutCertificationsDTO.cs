namespace PpeManager.Api.Application.DTO
{
    public record PpeWithoutCertificationsDTO
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }

        public PpeWithoutCertificationsDTO(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description; 
        }


        public static PpeWithoutCertificationsDTO FromEntity(Ppe ppe)
        {
            return new PpeWithoutCertificationsDTO(ppe.Id, ppe.Name.ToString(), ppe.Description.ToString());
        }
    }
}
