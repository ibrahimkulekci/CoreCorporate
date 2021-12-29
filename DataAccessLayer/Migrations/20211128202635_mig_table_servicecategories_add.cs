using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig_table_servicecategories_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    ServiceCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceCategoryTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceCategoryUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceCategoryContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceCategoryStatus = table.Column<bool>(type: "bit", nullable: false),
                    ServiceCategoryCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceCategoryUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategories", x => x.ServiceCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceCategoryID = table.Column<int>(type: "int", nullable: false),
                    ServiceTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceStatus = table.Column<bool>(type: "bit", nullable: false),
                    ServiceCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceCategories");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
