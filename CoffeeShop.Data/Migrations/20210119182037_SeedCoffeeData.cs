using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeShop.Data.Migrations
{
    public partial class SeedCoffeeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "Id", "ImageFileName", "Name", "Price", "Volume" },
                values: new object[,]
                {
                    { 1, "espresso.jpg", "Espresso", 8.0, "133 ml" },
                    { 2, "americano.jpg", "Americano", 12.0, "250 ml" },
                    { 3, "americano.jpg", "Americano", 15.0, "380 ml" },
                    { 4, "americano_with_milk.jpg", "Americano with milk", 14.0, "250 ml" },
                    { 5, "americano_with_milk.jpg", "Americano with milk", 17.0, "380 ml" },
                    { 6, "cappuccino.jpg", "Cappuccino", 15.0, "250 ml" },
                    { 7, "cappuccino.jpg", "Cappuccino", 18.0, "380 ml" },
                    { 8, "latte.jpg", "Latte", 15.0, "250 ml" },
                    { 9, "latte.jpg", "Latte", 18.0, "380 ml" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
