namespace PpeManager.Api.Application.CreatePpeCommand.Commands
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
