using Microsoft.EntityFrameworkCore.Migrations;

namespace Factory.Migrations
{
    public partial class ReviseNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EngineerName",
                table: "Engineers",
                newName: "EngineerLastName");

            migrationBuilder.AddColumn<string>(
                name: "EngineerFirstName",
                table: "Engineers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngineerFirstName",
                table: "Engineers");

            migrationBuilder.RenameColumn(
                name: "EngineerLastName",
                table: "Engineers",
                newName: "EngineerName");
        }
    }
}
