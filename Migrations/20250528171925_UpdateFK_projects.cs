using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRealWorld.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFK_projects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Pictures_Pictures_picturesId",
                table: "Project_Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Pictures_Projects_projectsId",
                table: "Project_Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsKW_KeyWords_keyWordsId",
                table: "ProjectsKW");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsKW_Projects_projectsId",
                table: "ProjectsKW");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Project_Pictures_picturesId",
                table: "Project_Pictures");

            migrationBuilder.DropColumn(
                name: "picturesId",
                table: "Project_Pictures");

            migrationBuilder.RenameColumn(
                name: "projectsId",
                table: "ProjectsKW",
                newName: "ProjectsId");

            migrationBuilder.RenameColumn(
                name: "keyWordsId",
                table: "ProjectsKW",
                newName: "KeyWordsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectsKW_projectsId",
                table: "ProjectsKW",
                newName: "IX_ProjectsKW_ProjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectsKW_keyWordsId",
                table: "ProjectsKW",
                newName: "IX_ProjectsKW_KeyWordsId");

            migrationBuilder.RenameColumn(
                name: "projectsId",
                table: "Project_Pictures",
                newName: "ProjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_Pictures_projectsId",
                table: "Project_Pictures",
                newName: "IX_Project_Pictures_ProjectsId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectsId",
                table: "ProjectsKW",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "KeyWordsId",
                table: "ProjectsKW",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectsId",
                table: "Project_Pictures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Pictures_Projects_ProjectsId",
                table: "Project_Pictures",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsKW_KeyWords_KeyWordsId",
                table: "ProjectsKW",
                column: "KeyWordsId",
                principalTable: "KeyWords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsKW_Projects_ProjectsId",
                table: "ProjectsKW",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Pictures_Projects_ProjectsId",
                table: "Project_Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsKW_KeyWords_KeyWordsId",
                table: "ProjectsKW");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsKW_Projects_ProjectsId",
                table: "ProjectsKW");

            migrationBuilder.RenameColumn(
                name: "ProjectsId",
                table: "ProjectsKW",
                newName: "projectsId");

            migrationBuilder.RenameColumn(
                name: "KeyWordsId",
                table: "ProjectsKW",
                newName: "keyWordsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectsKW_ProjectsId",
                table: "ProjectsKW",
                newName: "IX_ProjectsKW_projectsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectsKW_KeyWordsId",
                table: "ProjectsKW",
                newName: "IX_ProjectsKW_keyWordsId");

            migrationBuilder.RenameColumn(
                name: "ProjectsId",
                table: "Project_Pictures",
                newName: "projectsId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_Pictures_ProjectsId",
                table: "Project_Pictures",
                newName: "IX_Project_Pictures_projectsId");

            migrationBuilder.AlterColumn<int>(
                name: "projectsId",
                table: "ProjectsKW",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "keyWordsId",
                table: "ProjectsKW",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "projectsId",
                table: "Project_Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                    PathImg = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Pictures_Projects_projectsId",
                table: "Project_Pictures",
                column: "projectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
