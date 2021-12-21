namespace PpeManager.Api.Application.Command.CreatePpeCommand
{
    public class CreatePpeCommand: IRequest<PpeDTO>
    {
        public String Name { get;  set; }
        public String Description { get; set; }

        public CreatePpeCommand(String name, String description)
        {
            Name = name;
            Description = description;
        }
    }
}
