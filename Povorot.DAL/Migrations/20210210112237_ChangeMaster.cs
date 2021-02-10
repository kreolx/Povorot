using Microsoft.EntityFrameworkCore.Migrations;

namespace Povorot.DAL.Migrations
{
    public partial class ChangeMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phonenumber",
                table: "Mechanics",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phonenumber",
                table: "Mechanics");
        }
    }
}
