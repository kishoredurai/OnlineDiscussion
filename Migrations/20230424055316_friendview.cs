using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineDiscussion.Migrations
{
    public partial class friendview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FriendsView",
                columns: table => new
                {
                    FriendId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendsView", x => x.FriendId);
                    table.ForeignKey(
                        name: "FK_FriendsView_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FriendsView_TopicViewModel_TopicId",
                        column: x => x.TopicId,
                        principalTable: "TopicViewModel",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendsView_TopicId",
                table: "FriendsView",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendsView_UserId",
                table: "FriendsView",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendsView");
        }
    }
}
