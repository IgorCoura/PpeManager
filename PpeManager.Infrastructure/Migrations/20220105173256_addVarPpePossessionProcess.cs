using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PpeManager.Infrastructure.Migrations
{
    public partial class addVarPpePossessionProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOpenPpePossessionProcess",
                schema: "ppemanager",
                table: "workers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpenPpePossessionProcess",
                schema: "ppemanager",
                table: "workers");
        }
    }
}
