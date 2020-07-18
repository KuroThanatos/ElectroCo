using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroCo.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Funcionarios",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Funcionarios",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "91b48022-fcca-4aed-8bee-63f2ff93a8c5", 0, "bd1c4aa5-aaed-45ff-a6e9-11e8c6888644", "cliente@ipt.pt", true, false, null, "CLIENTE@IPT.PT", null, "AQAAAAEAACcQAAAAEOwjUR76Lx3fR0i9QH3Noni0nzQTLzJ9a2CM1v+IdBwB6ADWtKRgX4o4Sl8FyBIoqA==", null, false, "CYQGW2ATI3AOJUO66PHZWTHIPBZRU6NL", false, "cliente@ipt.pt" },
                    { "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816", 0, "bd1c4aa5-aaed-45ff-a6e9-11e8c6888644", "gestor@ipt.pt", true, false, null, "GESTOR@IPT.PT", null, "AQAAAAEAACcQAAAAEOwjUR76Lx3fR0i9QH3Noni0nzQTLzJ9a2CM1v+IdBwB6ADWtKRgX4o4Sl8FyBIoqA==", null, false, "CYQGW2ATI3AOJUO66PHZWTHIPBZRU6NL", false, "gestor@ipt.pt" },
                    { "f554eee4-e19d-4830-a02c-aabe9f18e8a7", 0, "bd1c4aa5-aaed-45ff-a6e9-11e8c6888644", "gerente@ipt.pt", true, false, null, "GERENTE@IPT.PT", null, "AQAAAAEAACcQAAAAEOwjUR76Lx3fR0i9QH3Noni0nzQTLzJ9a2CM1v+IdBwB6ADWtKRgX4o4Sl8FyBIoqA==", null, false, "CYQGW2ATI3AOJUO66PHZWTHIPBZRU6NL", false, "gerente@ipt.pt" }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ID", "CodigoPostal", "Email", "Morada", "NIF", "Nome", "Telefone", "UserId" },
                values: new object[] { 1, "2000-070 Almeirim", "cliente@ipt.pt", "Rua São João da Ribeira, nº59", "123456789", "Cliente Cliente", "987456123", "91b48022-fcca-4aed-8bee-63f2ff93a8c5" });

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "ID", "Email", "Nome", "NumFuncionario", "Telefone", "TipoFuncionario", "UserId", "password" },
                values: new object[,]
                {
                    { 2, "gestor@ipt.pt", "Gestor Gestor", 777, "987456123", "gestorArmazem", "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816", null },
                    { 1, "gerente@ipt.pt", "Gerente Gerente", 666, "987456123", "administrador", "f554eee4-e19d-4830-a02c-aabe9f18e8a7", null }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "ID", "EstadoProduto", "Imagem", "Nome", "Preco", "Stock", "Tipo" },
                values: new object[,]
                {
                    { 5, "Disponível", "5.png", "GSKILL 16GB 3600MHZ Cl18", 75f, 21, "Memórias RAM" },
                    { 6, "Disponível", "6.png", "SSD Samsung 750 500GB", 52f, 56, "Armazenamento Interno" },
                    { 7, "Disponível", "7.png", "SSD PNY 500GB", 48f, 34, "Armazenamento Interno" },
                    { 3, "Disponível", "3.png", "AMD Ryzen 3600", 186f, 3, "Processadores" },
                    { 9, "Disponível", "9.png", "Corsair Crystal 465X", 90f, 2, "Caixas de Computador" },
                    { 10, "Disponível", "10.png", "Water Cooler CoolerMaster MasterLiquid ML240L RGB", 50f, 12, "Coolers CPU" },
                    { 11, "Disponível", "11.png", "Corsair LL120 FAN Pack 3 ", 80f, 4, "Ventoinhas" },
                    { 12, "Disponível", "12.png", "ASUS B550 TUF motherboard", 135f, 1, "Motherboard" },
                    { 2, "Disponível", "2.png", "Asus zenphone", 320f, 10, "Smartphones" },
                    { 1, "Disponível", "1.png", "Msi b550 motherboard", 210f, 4, "Motherboard" },
                    { 4, "Indisponível", "4.png", "PSU Seasonic 650W Platinum", 112f, 0, "Fonte de Alimentação" },
                    { 8, "Disponível", "8.png", "ZOTAC NVIDIA RTX 2070 mini ", 450f, 2, "Placa Gráfica" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "f554eee4-e19d-4830-a02c-aabe9f18e8a7", "ad" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816", "ga" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "91b48022-fcca-4aed-8bee-63f2ff93a8c5", "c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "91b48022-fcca-4aed-8bee-63f2ff93a8c5", "c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816", "ga" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "f554eee4-e19d-4830-a02c-aabe9f18e8a7", "ad" });

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Funcionarios",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Funcionarios",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91b48022-fcca-4aed-8bee-63f2ff93a8c5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f554eee4-e19d-4830-a02c-aabe9f18e8a7");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);

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
    }
}
