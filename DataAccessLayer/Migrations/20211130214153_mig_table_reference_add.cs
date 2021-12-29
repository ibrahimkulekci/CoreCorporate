using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig_table_reference_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    ReferenceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceStatus = table.Column<bool>(type: "bit", nullable: false),
                    ReferenceCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenceUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.ReferenceID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "References");
        }
    }
}
