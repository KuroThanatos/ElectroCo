using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroCo.Migrations
{
    public partial class addUserIdClientesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Clientes",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad",
                column: "ConcurrencyStamp",
                value: "e8839932-7ea6-4499-a929-cebd23bb7cbb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "55f622b7-201d-4929-8d0f-37335ff4af06");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ga",
                column: "ConcurrencyStamp",
                value: "a09f4dc5-1ab3-43dd-b7c8-2fc0ad9312e9");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Clientes");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad",
                column: "ConcurrencyStamp",
                value: "1da3c985-b04b-4bb0-8aae-ed3e5014b0fb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "1171efd6-f581-4dbb-b71d-e2006397c0b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ga",
                column: "ConcurrencyStamp",
                value: "1a5c5980-cd01-484a-b92a-35ad9105ce3c");
        }
    }
}
