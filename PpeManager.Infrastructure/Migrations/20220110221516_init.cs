using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PpeManager.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ppemanager");

            migrationBuilder.CreateSequence(
                name: "companyseq",
                schema: "ppemanager",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "ppeCertificationseq",
                schema: "ppemanager",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "ppeseq",
                schema: "ppemanager",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "workerseq",
                schema: "ppemanager",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "companies",
                schema: "ppemanager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Cnpj = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "workers",
                schema: "ppemanager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Cpf = table.Column<string>(type: "text", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "text", nullable: false),
                    AdmissionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsOpenPpePossessionProcess = table.Column<bool>(type: "boolean", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PpesNotDelivered = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ppePossession",
                schema: "ppemanager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkerId = table.Column<int>(type: "integer", nullable: true),
                    PpeCertificationId = table.Column<int>(type: "integer", nullable: true),
                    DeliveryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Validity = table.Column<DateOnly>(type: "date", nullable: false),
                    Confirmation = table.Column<bool>(type: "boolean", nullable: false),
                    SupportingDocument = table.Column<string>(type: "text", nullable: true),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ppePossession_workers_WorkerId",
                        column: x => x.WorkerId,
                        principalSchema: "ppemanager",
                        principalTable: "workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ppeCertifications_PpeId",
                schema: "ppemanager",
                table: "ppeCertifications",
                column: "PpeId");

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
                name: "requests",
                schema: "ppemanager");

            migrationBuilder.DropTable(
                name: "ppeCertifications",
                schema: "ppemanager");

            migrationBuilder.DropTable(
                name: "workers",
                schema: "ppemanager");

            migrationBuilder.DropTable(
                name: "ppes",
                schema: "ppemanager");

            migrationBuilder.DropTable(
                name: "companies",
                schema: "ppemanager");

            migrationBuilder.DropSequence(
                name: "companyseq",
                schema: "ppemanager");

            migrationBuilder.DropSequence(
                name: "ppeCertificationseq",
                schema: "ppemanager");

            migrationBuilder.DropSequence(
                name: "ppeseq",
                schema: "ppemanager");

            migrationBuilder.DropSequence(
                name: "workerseq",
                schema: "ppemanager");
        }
    }
}
