using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PpeManager.Infrastructure.Migrations
{
    public partial class workerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "workerseq",
                schema: "ppemanager",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "workers",
                schema: "ppemanager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "text", nullable: false),
                    AdmissionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workers_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "ppemanager",
                        principalTable: "companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ppePossession",
                schema: "ppemanager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkerId = table.Column<int>(type: "integer", nullable: false),
                    PpeCertificationId = table.Column<int>(type: "integer", nullable: false),
                    DeliveryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Validity = table.Column<DateOnly>(type: "date", nullable: false),
                    Confirmation = table.Column<bool>(type: "boolean", nullable: false),
                    SupportingDocument = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ppePossession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ppePossession_ppeCertifications_PpeCertificationId",
                        column: x => x.PpeCertificationId,
                        principalSchema: "ppemanager",
                        principalTable: "ppeCertifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ppePossession_workers_WorkerId",
                        column: x => x.WorkerId,
                        principalSchema: "ppemanager",
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PpeWorker",
                schema: "ppemanager",
                columns: table => new
                {
                    PpesId = table.Column<int>(type: "integer", nullable: false),
                    WorkersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PpeWorker", x => new { x.PpesId, x.WorkersId });
                    table.ForeignKey(
                        name: "FK_PpeWorker_ppes_PpesId",
                        column: x => x.PpesId,
                        principalSchema: "ppemanager",
                        principalTable: "ppes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PpeWorker_workers_WorkersId",
                        column: x => x.WorkersId,
                        principalSchema: "ppemanager",
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ppePossession_PpeCertificationId",
                schema: "ppemanager",
                table: "ppePossession",
                column: "PpeCertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ppePossession_WorkerId",
                schema: "ppemanager",
                table: "ppePossession",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_PpeWorker_WorkersId",
                schema: "ppemanager",
                table: "PpeWorker",
                column: "WorkersId");

            migrationBuilder.CreateIndex(
                name: "IX_workers_CompanyId",
                schema: "ppemanager",
                table: "workers",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ppePossession",
                schema: "ppemanager");

            migrationBuilder.DropTable(
                name: "PpeWorker",
                schema: "ppemanager");

            migrationBuilder.DropTable(
                name: "workers",
                schema: "ppemanager");

            migrationBuilder.DropSequence(
                name: "workerseq",
                schema: "ppemanager");
        }
    }
}
