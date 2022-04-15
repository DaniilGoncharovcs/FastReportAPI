using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastReportAPI.Migrations
{
    public partial class UpdateName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_templates",
                table: "templates");

            migrationBuilder.RenameTable(
                name: "templates",
                newName: "Templates");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Templates",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Templates",
                table: "Templates",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Box");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Templates",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Templates");

            migrationBuilder.RenameTable(
                name: "Templates",
                newName: "templates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_templates",
                table: "templates",
                column: "Id");
        }
    }
}
