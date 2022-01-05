using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PpeManager.Infrastructure.Migrations
{
    public partial class addCpfInWorker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                schema: "ppemanager",
                table: "workers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SupportingDocument",
                schema: "ppemanager",
                table: "ppePossession",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                schema: "ppemanager",
                table: "workers");

            migrationBuilder.AlterColumn<string>(
                name: "SupportingDocument",
                schema: "ppemanager",
                table: "ppePossession",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
