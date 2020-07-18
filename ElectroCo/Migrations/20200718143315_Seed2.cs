using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroCo.Migrations
{
    public partial class Seed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad",
                column: "ConcurrencyStamp",
                value: "9c672dc1-6478-45bd-b1b6-1139ff5f8d79");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "a7ce4ba6-26c7-476a-9f67-75ac529b8b1c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ga",
                column: "ConcurrencyStamp",
                value: "7858d579-e075-4526-ada1-7f96ccd2f0d1");

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "Nome", "Gerente Gerente", "f554eee4-e19d-4830-a02c-aabe9f18e8a7" },
                    { 2, "Nome", "Gestor Gestor", "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816" },
                    { 3, "Nome", "Cliente Cliente", "91b48022-fcca-4aed-8bee-63f2ff93a8c5" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91b48022-fcca-4aed-8bee-63f2ff93a8c5",
                column: "NormalizedUserName",
                value: "CLIENTE@IPT.PT");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816",
                column: "NormalizedUserName",
                value: "GESTOR@IPT.PT");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f554eee4-e19d-4830-a02c-aabe9f18e8a7",
                column: "NormalizedUserName",
                value: "GERENTE@IPT.PT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad",
                column: "ConcurrencyStamp",
                value: "4bb53270-9740-43b2-911f-7d3842dea8d0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "e3f78b4e-a1c0-4f9a-8586-cb46e298c5b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ga",
                column: "ConcurrencyStamp",
                value: "fa1f64fc-7647-43a5-a8dd-f6e522103c90");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91b48022-fcca-4aed-8bee-63f2ff93a8c5",
                column: "NormalizedUserName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816",
                column: "NormalizedUserName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f554eee4-e19d-4830-a02c-aabe9f18e8a7",
                column: "NormalizedUserName",
                value: null);
        }
    }
}
