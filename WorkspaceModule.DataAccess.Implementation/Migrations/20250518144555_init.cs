using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkspaceModule.DataAccess.Implementation.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workspaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContentJson = table.Column<string>(type: "text", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                });

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
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    WorkspaceEntityId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "WorkspaceRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    FirstUnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    SecondUnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspaceRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkspaceRelations_WorkspaceUnits_FirstUnitId",
                        column: x => x.FirstUnitId,
                        principalTable: "WorkspaceUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkspaceRelations_WorkspaceUnits_SecondUnitId",
                        column: x => x.SecondUnitId,
                        principalTable: "WorkspaceUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkspaceRelations_Workspaces_WorkspaceEntityId",
                        column: x => x.WorkspaceEntityId,
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkspacePersons_WorkspaceEntityId",
                table: "WorkspacePersons",
                column: "WorkspaceEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceRelations_FirstUnitId",
                table: "WorkspaceRelations",
                column: "FirstUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceRelations_SecondUnitId",
                table: "WorkspaceRelations",
                column: "SecondUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceRelations_WorkspaceEntityId",
                table: "WorkspaceRelations",
                column: "WorkspaceEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceUnits_WorkspaceEntityId",
                table: "WorkspaceUnits",
                column: "WorkspaceEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkspacePersons");

            migrationBuilder.DropTable(
                name: "WorkspaceRelations");

            migrationBuilder.DropTable(
                name: "WorkspaceUnits");

            migrationBuilder.DropTable(
                name: "Workspaces");
        }
    }
}
