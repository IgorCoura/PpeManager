using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PpeManager.Infrastructure.Migrations
{
    public partial class ppeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ppemanager");

            migrationBuilder.CreateSequence(
                name: "ppeCertificationseq",
                schema: "ppemanager",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "ppeseq",
                schema: "ppemanager",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "ppes",
                schema: "ppemanager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ppes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ppeCertifications",
                schema: "ppemanager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    PpeId = table.Column<int>(type: "integer", nullable: false),
                    ApprovalCertificateNumber = table.Column<string>(type: "text", nullable: false),
                    Validity = table.Column<DateOnly>(type: "date", nullable: false),
                    Durability = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ppeCertifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ppeCertifications_ppes_PpeId",
                        column: x => x.PpeId,
                        principalSchema: "ppemanager",
                        principalTable: "ppes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ppeCertifications_PpeId",
                schema: "ppemanager",
                table: "ppeCertifications",
                column: "PpeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ppeCertifications",
                schema: "ppemanager");

            migrationBuilder.DropTable(
                name: "ppes",
                schema: "ppemanager");

            migrationBuilder.DropSequence(
                name: "ppeCertificationseq",
                schema: "ppemanager");

            migrationBuilder.DropSequence(
                name: "ppeseq",
                schema: "ppemanager");
        }
    }
}
