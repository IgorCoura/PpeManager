namespace PpeManager.Api.Application.DTO
{
    public record PpeDTO
    {
        public int Id { get; }
        public String Name { get; }
        public String Description { get; }
        public List<PpeCertificationDTO>? PpeCertifications { get; }

        public PpeDTO(int id, string name, string description, List<PpeCertificationDTO>? ppeCertifications)
        {
            Id = id;
            Name = name;
            Description = description;
            PpeCertifications = ppeCertifications;
        }

        public bool Equal(PpeDTO entity) => Id == Id && Name == Name && Description == Description && PpeCertifications.SequenceEqual(entity.PpeCertifications);

        public static PpeDTO FromEntity(Ppe ppe)
        {
            return new PpeDTO(
                ppe.Id, 
                ppe.Name.ToString(), 
                ppe.Description.ToString(), 
                ppe.PpeCertifications?.Select(p => PpeCertificationDTO.FromEntity(p)).ToList());
        }

    }
}
