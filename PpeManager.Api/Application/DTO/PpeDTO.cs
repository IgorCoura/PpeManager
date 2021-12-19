namespace PpeManager.Api.Application.DTO
{
    public record PpeDTO
    {
        public int Id { get; }
        public String Name { get; }
        public String Description { get; }

        public PpeDTO(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

    }
}
