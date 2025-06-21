using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workspace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeuselessentites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceRelations_WorkspaceUnits_FirstUnitId",
                table: "WorkspaceRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceRelations_WorkspaceUnits_SecondUnitId",
                table: "WorkspaceRelations");

            migrationBuilder.DropTable(
                name: "WorkspacePersons");

            migrationBuilder.DropTable(
                name: "WorkspaceUnits");

            migrationBuilder.DropIndex(
                name: "IX_WorkspaceRelations_FirstUnitId",
                table: "WorkspaceRelations");

            migrationBuilder.DropIndex(
                name: "IX_WorkspaceRelations_SecondUnitId",
                table: "WorkspaceRelations");

            migrationBuilder.RenameColumn(
                name: "SecondUnitId",
                table: "WorkspaceRelations",
                newName: "SecondSocialNode");

            migrationBuilder.RenameColumn(
                name: "FirstUnitId",
                table: "WorkspaceRelations",
                newName: "FirstSocialNode");

            migrationBuilder.AddColumn<Guid[]>(
                name: "Events",
                table: "Workspaces",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0]);

            migrationBuilder.AddColumn<Guid[]>(
                name: "SocialNodes",
                table: "Workspaces",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0]);

            migrationBuilder.AddColumn<Guid[]>(
                name: "Tasks",
                table: "Workspaces",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Events",
                table: "Workspaces");

            migrationBuilder.DropColumn(
                name: "SocialNodes",
                table: "Workspaces");

            migrationBuilder.DropColumn(
                name: "Tasks",
                table: "Workspaces");

            migrationBuilder.RenameColumn(
                name: "SecondSocialNode",
                table: "WorkspaceRelations",
                newName: "SecondUnitId");

            migrationBuilder.RenameColumn(
                name: "FirstSocialNode",
                table: "WorkspaceRelations",
                newName: "FirstUnitId");

            migrationBuilder.CreateTable(
                name: "WorkspacePersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspacePersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkspacePersons_Workspaces_WorkspaceEntityId",
                        column: x => x.WorkspaceEntityId,
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkspaceUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspaceUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkspaceUnits_Workspaces_WorkspaceEntityId",
                        column: x => x.WorkspaceEntityId,
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceRelations_FirstUnitId",
                table: "WorkspaceRelations",
                column: "FirstUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceRelations_SecondUnitId",
                table: "WorkspaceRelations",
                column: "SecondUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspacePersons_WorkspaceEntityId",
                table: "WorkspacePersons",
                column: "WorkspaceEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceUnits_WorkspaceEntityId",
                table: "WorkspaceUnits",
                column: "WorkspaceEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceRelations_WorkspaceUnits_FirstUnitId",
                table: "WorkspaceRelations",
                column: "FirstUnitId",
                principalTable: "WorkspaceUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceRelations_WorkspaceUnits_SecondUnitId",
                table: "WorkspaceRelations",
                column: "SecondUnitId",
                principalTable: "WorkspaceUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
