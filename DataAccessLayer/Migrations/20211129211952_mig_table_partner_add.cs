using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig_table_partner_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    PartnerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartnerTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerStatus = table.Column<bool>(type: "bit", nullable: false),
                    PartnerCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartnerUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.PartnerID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partners");
        }
    }
}
