using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimeMacrocosm.Migrations
{
    public partial class AddedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostCreator",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserRefId",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserEmailAddress = table.Column<string>(nullable: true),
                    UserScreenName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ApplicationUserRefId",
                table: "Posts",
                column: "ApplicationUserRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_User_ApplicationUserRefId",
                table: "Posts",
                column: "ApplicationUserRefId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_User_ApplicationUserRefId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ApplicationUserRefId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserRefId",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "PostCreator",
                table: "Posts",
                maxLength: 30,
                nullable: true);
        }
    }
}
