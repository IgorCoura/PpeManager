namespace PpeManager.Api.DTO
{
    public record CreatePpeDTO
    {
        public String Name { get; }
        public String Description { get; }
        public CreatePpeDTO(string name, string description)
        {
            Name = name;
            Description = description;
        }


    }
}
