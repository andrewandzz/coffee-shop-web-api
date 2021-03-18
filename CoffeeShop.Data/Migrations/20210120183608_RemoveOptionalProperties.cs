using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeShop.Data.Migrations
{
    public partial class RemoveOptionalProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Orders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Volume",
                table: "Coffees",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coffees",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageFileName",
                table: "Coffees",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageFileName",
                value: "espresso.webp");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageFileName",
                value: "americano.webp");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageFileName",
                value: "americano.webp");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageFileName",
                value: "americano-with-milk.webp");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageFileName",
                value: "americano-with-milk.webp");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageFileName",
                value: "cappuccino.webp");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageFileName",
                value: "cappuccino.webp");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageFileName",
                value: "latte.webp");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageFileName",
                value: "latte.webp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Orders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Volume",
                table: "Coffees",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coffees",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "ImageFileName",
                table: "Coffees",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageFileName",
                value: "espresso.jpg");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageFileName",
                value: "americano.jpg");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageFileName",
                value: "americano.jpg");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageFileName",
                value: "americano_with_milk.jpg");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageFileName",
                value: "americano_with_milk.jpg");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageFileName",
                value: "cappuccino.jpg");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageFileName",
                value: "cappuccino.jpg");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageFileName",
                value: "latte.jpg");

            migrationBuilder.UpdateData(
                table: "Coffees",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageFileName",
                value: "latte.jpg");
        }
    }
}
