using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimeMacrocosm.Migrations
{
    public partial class AddedSeriesImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_ApplicationUserRefId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "AnimeItems");

            migrationBuilder.DropTable(
                name: "MangaItems");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ApplicationUserRefId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserRefId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Images",
                newName: "ImageId");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "ProductionStudios",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageCaption",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageDimensions",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFormat",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Distributors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeriesItemId",
                table: "CreatorAuthors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SeriesImages",
                columns: table => new
                {
                    SeriesId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesImages", x => new { x.SeriesId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_SeriesImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesImages_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "SeriesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SeriesId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProductionId = table.Column<int>(nullable: false),
                    DistributorId = table.Column<int>(nullable: false),
                    CreatorAuthorId = table.Column<int>(nullable: false),
                    Length = table.Column<string>(nullable: true),
                    FormatId = table.Column<int>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeriesItems_Distributors_DistributorId",
                        column: x => x.DistributorId,
                        principalTable: "Distributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesItems_Formats_FormatId",
                        column: x => x.FormatId,
                        principalTable: "Formats",
                        principalColumn: "FormatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesItems_ProductionStudios_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "ProductionStudios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesItems_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "SeriesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesItemImages",
                columns: table => new
                {
                    SeriesItemId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesItemImages", x => new { x.SeriesItemId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_SeriesItemImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesItemImages_SeriesItems_SeriesItemId",
                        column: x => x.SeriesItemId,
                        principalTable: "SeriesItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatorAuthors_SeriesItemId",
                table: "CreatorAuthors",
                column: "SeriesItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesImages_ImageId",
                table: "SeriesImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesItemImages_ImageId",
                table: "SeriesItemImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesItems_DistributorId",
                table: "SeriesItems",
                column: "DistributorId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesItems_FormatId",
                table: "SeriesItems",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesItems_ProductionId",
                table: "SeriesItems",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesItems_SeriesId",
                table: "SeriesItems",
                column: "SeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreatorAuthors_SeriesItems_SeriesItemId",
                table: "CreatorAuthors",
                column: "SeriesItemId",
                principalTable: "SeriesItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreatorAuthors_SeriesItems_SeriesItemId",
                table: "CreatorAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "SeriesImages");

            migrationBuilder.DropTable(
                name: "SeriesItemImages");

            migrationBuilder.DropTable(
                name: "SeriesItems");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_CreatorAuthors_SeriesItemId",
                table: "CreatorAuthors");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "ProductionStudios");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageCaption",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageDimensions",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageFormat",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Distributors");

            migrationBuilder.DropColumn(
                name: "SeriesItemId",
                table: "CreatorAuthors");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Images",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserRefId",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnimeItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatorAuthorId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DistributorId = table.Column<int>(nullable: false),
                    FormatId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    ProductionId = table.Column<int>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: true),
                    RunTime = table.Column<string>(nullable: true),
                    SeriesId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MangaItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatorAuthorId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DistributorId = table.Column<int>(nullable: false),
                    FormatId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    PageCount = table.Column<int>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: true),
                    SeriesId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ApplicationUserRefId",
                table: "Posts",
                column: "ApplicationUserRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_ApplicationUserRefId",
                table: "Posts",
                column: "ApplicationUserRefId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
