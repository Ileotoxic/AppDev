using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Migrations
{
    /// <inheritdoc />
    public partial class initdb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationModel",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobListingId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationModel", x => x.ApplicationId);
                });

            migrationBuilder.CreateTable(
                name: "JobListingModel",
                columns: table => new
                {
                    JobListingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerApplicationId = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobListingModel", x => x.JobListingId);
                    table.ForeignKey(
                        name: "FK_JobListingModel_ApplicationModel_EmployerApplicationId",
                        column: x => x.EmployerApplicationId,
                        principalTable: "ApplicationModel",
                        principalColumn: "ApplicationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobListingModel_EmployerApplicationId",
                table: "JobListingModel",
                column: "EmployerApplicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobListingModel");

            migrationBuilder.DropTable(
                name: "ApplicationModel");
        }
    }
}
