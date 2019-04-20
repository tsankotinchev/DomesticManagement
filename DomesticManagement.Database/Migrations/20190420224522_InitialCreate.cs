using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DomesticManagement.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DomesticResponsibilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ResponsibilityName = table.Column<string>(nullable: true),
                    ResponsibilityDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomesticResponsibilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DomesticResponsibilityOccurances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    DomesticResponsibilityId = table.Column<Guid>(nullable: false),
                    OccuranceDateTime = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomesticResponsibilityOccurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomesticResponsibilityOccurances_DomesticResponsibilities_DomesticResponsibilityId",
                        column: x => x.DomesticResponsibilityId,
                        principalTable: "DomesticResponsibilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DomesticResponsibilityOccurances_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomesticResponsibilityOccurances_DomesticResponsibilityId",
                table: "DomesticResponsibilityOccurances",
                column: "DomesticResponsibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_DomesticResponsibilityOccurances_UserId",
                table: "DomesticResponsibilityOccurances",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomesticResponsibilityOccurances");

            migrationBuilder.DropTable(
                name: "DomesticResponsibilities");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
