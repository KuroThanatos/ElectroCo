using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroCo.Migrations
{
    public partial class fixFuncionarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Funcionarios");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Funcionarios",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Funcionarios");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad",
                column: "ConcurrencyStamp",
                value: "11181315-ccc1-4259-b0a8-ae529c8911c0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "0f1d61ed-41eb-4fb0-ac04-ae7b02a4293a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ga",
                column: "ConcurrencyStamp",
                value: "8421d3ab-34cc-4dbf-8046-e65432d447b1");
        }
    }
}
