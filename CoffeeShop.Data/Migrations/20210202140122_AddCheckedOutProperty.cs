using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeShop.Data.Migrations
{
    public partial class AddCheckedOutProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CheckedOut",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckedOut",
                table: "Orders");
        }
    }
}
