using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Ecomm_Offical.DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Satate",
                table: "AspNetUsers",
                newName: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "AspNetUsers",
                newName: "Satate");
        }
    }
}
