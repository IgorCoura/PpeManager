using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PpeManager.Infrastructure.Migrations
{
    public partial class clientRequestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "requests",
                schema: "ppemanager",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "requests",
                schema: "ppemanager");
        }
    }
}
