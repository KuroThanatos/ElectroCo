using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroCo.Migrations
{
    public partial class addQuantidadeShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "ShoppingCart",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad",
                column: "ConcurrencyStamp",
                value: "8775105f-80d1-49ce-89c3-ff2b00ad3fa2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "be2de6f7-22cc-49b2-a7d3-ed981d46c827");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ga",
                column: "ConcurrencyStamp",
                value: "f5dd0d7a-b072-4af3-b190-b2e9e6f4bcf5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "ShoppingCart");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad",
                column: "ConcurrencyStamp",
                value: "7fd5665a-485f-4ed5-a884-b604b3fc7eeb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "d7d0b022-be39-4209-a6df-e0cb119d4cbf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ga",
                column: "ConcurrencyStamp",
                value: "131fc403-0722-4d0e-a439-54b68b538870");
        }
    }
}
