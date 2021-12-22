namespace PpeManager.Api.Application.Command.CreatePpeCommand
{
    public class CreatePpeCommand: IRequest<PpeDTO>
    {
        public string Name { get;  set; }
        public string Description { get; set; }

        public CreatePpeCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
