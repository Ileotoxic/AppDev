using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookShop.Migrations
{
    /// <inheritdoc />
    public partial class CreatingAndSeedingTablesForProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Categories");

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
                    CategoryId = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_JobListingModel_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "A Back End Developer is responsible for server-side application logic and integration of the work front-end developers do.", "Back-End Developer" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "A Front End Developer is focused on the user interface and user experience of a website or application.", "Front-End Developer" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "A Full Stack Developer is capable of working on both the front-end and back-end portions of an application.", "Full Stack Developer" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "A Mobile Apps Developer is specialized in creating applications for mobile devices, such as smartphones and tablets.", "Mobile Apps Developer" });

            migrationBuilder.InsertData(
                table: "JobListingModel",
                columns: new[] { "JobListingId", "ApplicationDeadline", "CategoryId", "Description", "EmployerApplicationId", "Image", "Location", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 28, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Hello", null, null, "NY", "C# Programming" },
                    { 2, new DateTime(2024, 4, 28, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Learning Harder", null, null, "NY", "Advanced Programming" },
                    { 3, new DateTime(2024, 4, 28, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Basic language", null, null, "NY", "Java Programming" },
                    { 4, new DateTime(2024, 4, 28, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Really not easy", null, null, "NY", "Data Structures" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobListingModel_CategoryId",
                table: "JobListingModel",
                column: "CategoryId");

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

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Microsoft", 1, "Hello", null, 10.0, "C# Programming" },
                    { 2, "BTEC", 2, "Learning Harder", null, 11.0, "Advanced Programming" },
                    { 3, "Sun", 3, "Basic language", null, 15.0, "Java Programming" },
                    { 4, "Greenwich", 1, "Really not easy", null, 20.0, "Data Structures" },
                    { 5, "Microsoft", 2, "Now", null, 10.0, "App Dev" }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "DisplayOrder", "Name" },
                values: new object[] { "So funny", 0, "Adventure" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "DisplayOrder", "Name" },
                values: new object[] { "So romantic", 0, "Roman" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "DisplayOrder", "Name" },
                values: new object[] { "So scary", 0, "Horror" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "DisplayOrder", "Name" },
                values: new object[] { "So Boring", 0, "Science" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");
        }
    }
}
