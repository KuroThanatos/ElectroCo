using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroCo.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "fe4fa11e-4896-41c1-97a3-8f817355d7ed", "administrador" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "1e61cba7-3cc3-47d5-82a1-08597bf63670", "cliente" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ga",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "32d80c83-b118-48ec-ae99-b08095cc7914", "gestorArmazem" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "e8839932-7ea6-4499-a929-cebd23bb7cbb", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "55f622b7-201d-4929-8d0f-37335ff4af06", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ga",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "a09f4dc5-1ab3-43dd-b7c8-2fc0ad9312e9", null });
        }
    }
}
