using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserModule.DataAccess.Implementation.Migrations
{
    /// <inheritdoc />
    public partial class smallupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatInstance",
                table: "UserSettings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatInstance",
                table: "UserSettings");
        }
    }
}
