using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneCompany.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumberId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Price = table.Column<float>(type: "real", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TariffPlanEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Price = table.Column<float>(type: "real", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffPlanEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumberEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false, defaultValue: "Inactive"),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    TariffPalnId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumberEntity_CustomerEntity_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PhoneNumberEntity_TariffPlanEntity_TariffPalnId",
                        column: x => x.TariffPalnId,
                        principalTable: "TariffPlanEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TariffPlanServiceEntity",
                columns: table => new
                {
                    TariffPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffPlanServiceEntity", x => new { x.TariffPlanId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_TariffPlanServiceEntity_ServiceEntity_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TariffPlanServiceEntity_TariffPlanEntity_TariffPlanId",
                        column: x => x.TariffPlanId,
                        principalTable: "TariffPlanEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumberEntity_CustomerId",
                table: "PhoneNumberEntity",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumberEntity_TariffPalnId",
                table: "PhoneNumberEntity",
                column: "TariffPalnId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffPlanServiceEntity_ServiceId",
                table: "TariffPlanServiceEntity",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneNumberEntity");

            migrationBuilder.DropTable(
                name: "TariffPlanServiceEntity");

            migrationBuilder.DropTable(
                name: "CustomerEntity");

            migrationBuilder.DropTable(
                name: "ServiceEntity");

            migrationBuilder.DropTable(
                name: "TariffPlanEntity");
        }
    }
}
