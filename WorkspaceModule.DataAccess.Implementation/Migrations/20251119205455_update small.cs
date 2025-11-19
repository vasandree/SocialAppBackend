using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkspaceModule.DataAccess.Implementation.Migrations
{
    /// <inheritdoc />
    public partial class updatesmall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Workspaces",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Workspaces",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "WorkspaceRelations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WorkspaceRelations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorkspaceRelations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Workspaces");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Workspaces");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "WorkspaceRelations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WorkspaceRelations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorkspaceRelations");
        }
    }
}
