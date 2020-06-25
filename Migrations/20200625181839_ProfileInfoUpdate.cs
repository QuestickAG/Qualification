using Microsoft.EntityFrameworkCore.Migrations;

namespace Qualification.Migrations
{
    public partial class ProfileInfoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "ProfileInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoryOfWork",
                table: "ProfileInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Education",
                table: "ProfileInfos");

            migrationBuilder.DropColumn(
                name: "HistoryOfWork",
                table: "ProfileInfos");
        }
    }
}
