using Microsoft.EntityFrameworkCore.Migrations;

namespace TheFoodStapleEx.Data.Migrations
{
    public partial class CreatetableFMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPreferredItem",
                table: "Items",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPreferredItem",
                table: "Items");
        }
    }
}
