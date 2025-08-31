using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkspaceModule.DataAccess.Implementation.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceRelations_Workspaces_WorkspaceEntityId",
                table: "WorkspaceRelations");

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

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "WorkspaceRelations");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkspaceEntityId",
                table: "WorkspaceRelations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceRelations_Workspaces_WorkspaceEntityId",
                table: "WorkspaceRelations",
                column: "WorkspaceEntityId",
                principalTable: "Workspaces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceRelations_Workspaces_WorkspaceEntityId",
                table: "WorkspaceRelations");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkspaceEntityId",
                table: "WorkspaceRelations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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

            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "WorkspaceRelations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceRelations_Workspaces_WorkspaceEntityId",
                table: "WorkspaceRelations",
                column: "WorkspaceEntityId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
