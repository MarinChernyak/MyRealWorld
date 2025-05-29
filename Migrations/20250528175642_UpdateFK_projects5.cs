using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRealWorld.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFK_projects5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "picturesId",
                table: "Project_Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionImg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_Pictures_picturesId",
                table: "Project_Pictures",
                column: "picturesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Pictures_Pictures_picturesId",
                table: "Project_Pictures",
                column: "picturesId",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Pictures_Pictures_picturesId",
                table: "Project_Pictures");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Project_Pictures_picturesId",
                table: "Project_Pictures");

            migrationBuilder.DropColumn(
                name: "picturesId",
                table: "Project_Pictures");
        }
    }
}
