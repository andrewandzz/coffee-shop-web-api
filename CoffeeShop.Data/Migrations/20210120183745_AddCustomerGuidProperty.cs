using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeShop.Data.Migrations
{
    public partial class AddCustomerGuidProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerGuid",
                table: "Orders",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerGuid",
                table: "Orders");
        }
    }
}
