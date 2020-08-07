using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiTenantTest.Migrations
{
    public partial class initTenants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Objects",
                table: "Objects");

            migrationBuilder.RenameTable(
                name: "Objects",
                newName: "TestObjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestObjects",
                table: "TestObjects",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TestObjects",
                table: "TestObjects");

            migrationBuilder.RenameTable(
                name: "TestObjects",
                newName: "Objects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Objects",
                table: "Objects",
                column: "ID");
        }
    }
}
