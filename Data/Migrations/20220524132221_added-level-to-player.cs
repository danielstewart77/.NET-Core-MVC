using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quintrix_Web_App_Core_MVC.Data.Migrations
{
    public partial class addedleveltoplayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Players");
        }
    }
}
