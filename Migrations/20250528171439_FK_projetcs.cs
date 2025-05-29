using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRealWorld.Migrations
{
    /// <inheritdoc />
    public partial class FK_projetcs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "keyWordsId",
                table: "ProjectsKW",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "projectsId",
                table: "ProjectsKW",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PathImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionImg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project_Pictures",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    PictureID = table.Column<int>(type: "int", nullable: false),
                    projectsId = table.Column<int>(type: "int", nullable: false),
                    picturesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project_Pictures", x => new { x.ProjectId, x.PictureID });
                    table.ForeignKey(
                        name: "FK_Project_Pictures_Pictures_picturesId",
                        column: x => x.picturesId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project_Pictures_Projects_projectsId",
                        column: x => x.projectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsKW_keyWordsId",
                table: "ProjectsKW",
                column: "keyWordsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsKW_projectsId",
                table: "ProjectsKW",
                column: "projectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Pictures_picturesId",
                table: "Project_Pictures",
                column: "picturesId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Pictures_projectsId",
                table: "Project_Pictures",
                column: "projectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsKW_KeyWords_keyWordsId",
                table: "ProjectsKW",
                column: "keyWordsId",
                principalTable: "KeyWords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsKW_Projects_projectsId",
                table: "ProjectsKW",
                column: "projectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsKW_KeyWords_keyWordsId",
                table: "ProjectsKW");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsKW_Projects_projectsId",
                table: "ProjectsKW");

            migrationBuilder.DropTable(
                name: "Project_Pictures");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_ProjectsKW_keyWordsId",
                table: "ProjectsKW");

            migrationBuilder.DropIndex(
                name: "IX_ProjectsKW_projectsId",
                table: "ProjectsKW");

            migrationBuilder.DropColumn(
                name: "keyWordsId",
                table: "ProjectsKW");

            migrationBuilder.DropColumn(
                name: "projectsId",
                table: "ProjectsKW");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
