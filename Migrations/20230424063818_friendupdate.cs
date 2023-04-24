using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineDiscussion.Migrations
{
    public partial class friendupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "FriendsView",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "FriendsView");
        }
    }
}
