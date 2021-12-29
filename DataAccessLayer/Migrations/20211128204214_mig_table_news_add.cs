using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig_table_news_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsStatus = table.Column<bool>(type: "bit", nullable: false),
                    NewsCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NewsUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");
        }
    }
}
