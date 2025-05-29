using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRealWorld.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNames_projects_pictures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Pictures_Pictures_picturesId",
                table: "Project_Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Pictures_Projects_ProjectsId",
                table: "Project_Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsKW_Projects_ProjectsId",
                table: "ProjectsKW");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_ProjectsKW_ProjectsId",
                table: "ProjectsKW");

            migrationBuilder.DropIndex(
                name: "IX_Project_Pictures_picturesId",
                table: "Project_Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Project_Pictures_ProjectsId",
                table: "Project_Pictures");

            migrationBuilder.DropColumn(
                name: "ProjectsId",
                table: "ProjectsKW");

            migrationBuilder.DropColumn(
                name: "ProjectsId",
                table: "Project_Pictures");

            migrationBuilder.DropColumn(
                name: "picturesId",
                table: "Project_Pictures");

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionImg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_Pictures_PictureID",
                table: "Project_Pictures",
                column: "PictureID");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Pictures_Picture_PictureID",
                table: "Project_Pictures",
                column: "PictureID",
                principalTable: "Picture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Pictures_Projects_ProjectId",
                table: "Project_Pictures",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsKW_Projects_ProjectId",
                table: "ProjectsKW",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Pictures_Picture_PictureID",
                table: "Project_Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Pictures_Projects_ProjectId",
                table: "Project_Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsKW_Projects_ProjectId",
                table: "ProjectsKW");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_Project_Pictures_PictureID",
                table: "Project_Pictures");

            migrationBuilder.AddColumn<int>(
                name: "ProjectsId",
                table: "ProjectsKW",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectsId",
                table: "Project_Pictures",
                type: "int",
                nullable: true);

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
                    DescriptionImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlImg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsKW_ProjectsId",
                table: "ProjectsKW",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Pictures_picturesId",
                table: "Project_Pictures",
                column: "picturesId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Pictures_ProjectsId",
                table: "Project_Pictures",
                column: "ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Pictures_Pictures_picturesId",
                table: "Project_Pictures",
                column: "picturesId",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Pictures_Projects_ProjectsId",
                table: "Project_Pictures",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsKW_Projects_ProjectsId",
                table: "ProjectsKW",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
