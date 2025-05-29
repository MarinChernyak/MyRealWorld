using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRealWorld.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFK_projects2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Default",
                table: "KeyWords",
                newName: "DefaultEn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefaultEn",
                table: "KeyWords",
                newName: "Default");
        }
    }
}
