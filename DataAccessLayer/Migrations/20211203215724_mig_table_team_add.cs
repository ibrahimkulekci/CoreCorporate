using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig_table_team_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamStatus = table.Column<bool>(type: "bit", nullable: false),
                    TeamCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeamUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
