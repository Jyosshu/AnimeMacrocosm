using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimeMacrocosm.Migrations
{
    public partial class AddSeriesCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorAuthorId",
                table: "Series");

            migrationBuilder.CreateTable(
                name: "Formats",
                columns: table => new
                {
                    FormatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormatName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formats", x => x.FormatId);
                });

            migrationBuilder.CreateTable(
                name: "SeriesCreators",
                columns: table => new
                {
                    SeriesId = table.Column<int>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesCreators", x => new { x.CreatorId, x.SeriesId });
                    table.ForeignKey(
                        name: "FK_SeriesCreators_CreatorAuthors_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "CreatorAuthors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesCreators_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "SeriesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeriesCreators_SeriesId",
                table: "SeriesCreators",
                column: "SeriesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Formats");

            migrationBuilder.DropTable(
                name: "SeriesCreators");

            migrationBuilder.AddColumn<int>(
                name: "CreatorAuthorId",
                table: "Series",
                nullable: false,
                defaultValue: 0);
        }
    }
}
