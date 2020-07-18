using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroCo.Migrations
{
    public partial class FullDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 70, nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(maxLength: 9, nullable: false),
                    NIF = table.Column<string>(maxLength: 9, nullable: false),
                    Morada = table.Column<string>(nullable: false),
                    CodigoPostal = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 70, nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(maxLength: 9, nullable: false),
                    NumFuncionario = table.Column<int>(nullable: false),
                    TipoFuncionario = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 70, nullable: false),
                    Tipo = table.Column<string>(nullable: false),
                    Preco = table.Column<float>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    EstadoProduto = table.Column<string>(nullable: false),
                    Imagem = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Encomendas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoEncomenda = table.Column<string>(nullable: true),
                    DataEncomenda = table.Column<DateTime>(nullable: false),
                    MoradaEncomenda = table.Column<string>(nullable: true),
                    MoradaFaturacao = table.Column<string>(nullable: true),
                    PrevisaoEntrega = table.Column<DateTime>(nullable: false),
                    TrackID = table.Column<string>(nullable: true),
                    ClientID = table.Column<int>(nullable: false),
                    GestorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encomendas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Encomendas_Clientes_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clientes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encomendas_Funcionarios_GestorID",
                        column: x => x.GestorID,
                        principalTable: "Funcionarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantidade = table.Column<int>(nullable: false),
                    ClientID = table.Column<int>(nullable: false),
                    ProdutoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShoppingCart_Clientes_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clientes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCart_Produtos_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produtos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalhesEncomendas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantidade = table.Column<int>(nullable: false),
                    PrecoProduto = table.Column<float>(nullable: false),
                    EncomendaID = table.Column<int>(nullable: false),
                    ProdutoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalhesEncomendas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DetalhesEncomendas_Encomendas_EncomendaID",
                        column: x => x.EncomendaID,
                        principalTable: "Encomendas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalhesEncomendas_Produtos_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produtos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c", "51fffe77-cb44-4625-b6cc-06c66a413bf1", "cliente", "cliente" },
                    { "ga", "ad4d8f2d-cd50-4d1e-8ac5-c2991545e9e1", "gestorArmazem", "gestorArmazem" },
                    { "ad", "42ca3339-57fa-4f5a-811f-c78d4003ae30", "administrador", "administrador" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "91b48022-fcca-4aed-8bee-63f2ff93a8c5", 0, "bd1c4aa5-aaed-45ff-a6e9-11e8c6888644", "cliente@ipt.pt", true, false, null, "CLIENTE@IPT.PT", "CLIENTE@IPT.PT", "AQAAAAEAACcQAAAAEOwjUR76Lx3fR0i9QH3Noni0nzQTLzJ9a2CM1v+IdBwB6ADWtKRgX4o4Sl8FyBIoqA==", null, false, "CYQGW2ATI3AOJUO66PHZWTHIPBZRU6NL", false, "cliente@ipt.pt" },
                    { "f554eee4-e19d-4830-a02c-aabe9f18e8a7", 0, "bd1c4aa5-aaed-45ff-a6e9-11e8c6888644", "gerente@ipt.pt", true, false, null, "GERENTE@IPT.PT", "GERENTE@IPT.PT", "AQAAAAEAACcQAAAAEOwjUR76Lx3fR0i9QH3Noni0nzQTLzJ9a2CM1v+IdBwB6ADWtKRgX4o4Sl8FyBIoqA==", null, false, "CYQGW2ATI3AOJUO66PHZWTHIPBZRU6NL", false, "gerente@ipt.pt" },
                    { "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816", 0, "bd1c4aa5-aaed-45ff-a6e9-11e8c6888644", "gestor@ipt.pt", true, false, null, "GESTOR@IPT.PT", "GESTOR@IPT.PT", "AQAAAAEAACcQAAAAEOwjUR76Lx3fR0i9QH3Noni0nzQTLzJ9a2CM1v+IdBwB6ADWtKRgX4o4Sl8FyBIoqA==", null, false, "CYQGW2ATI3AOJUO66PHZWTHIPBZRU6NL", false, "gestor@ipt.pt" }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ID", "CodigoPostal", "Email", "Morada", "NIF", "Nome", "Telefone", "UserId" },
                values: new object[] { 1, "2000-070 Almeirim", "cliente@ipt.pt", "Rua São João da Ribeira, nº59", "123456789", "Cliente Cliente", "987456123", "91b48022-fcca-4aed-8bee-63f2ff93a8c5" });

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "ID", "Email", "Nome", "NumFuncionario", "Password", "Telefone", "TipoFuncionario", "UserId" },
                values: new object[,]
                {
                    { 2, "gestor@ipt.pt", "Gestor Gestor", 777, null, "987456123", "gestorArmazem", "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816" },
                    { 1, "gerente@ipt.pt", "Gerente Gerente", 666, null, "987456123", "administrador", "f554eee4-e19d-4830-a02c-aabe9f18e8a7" }
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
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "Nome", "Gerente Gerente", "f554eee4-e19d-4830-a02c-aabe9f18e8a7" },
                    { 2, "Nome", "Gestor Gestor", "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816" },
                    { 3, "Nome", "Cliente Cliente", "91b48022-fcca-4aed-8bee-63f2ff93a8c5" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "f554eee4-e19d-4830-a02c-aabe9f18e8a7", "ad" },
                    { "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816", "ga" },
                    { "91b48022-fcca-4aed-8bee-63f2ff93a8c5", "c" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesEncomendas_EncomendaID",
                table: "DetalhesEncomendas",
                column: "EncomendaID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesEncomendas_ProdutoID",
                table: "DetalhesEncomendas",
                column: "ProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Encomendas_ClientID",
                table: "Encomendas",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Encomendas_GestorID",
                table: "Encomendas",
                column: "GestorID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ClientID",
                table: "ShoppingCart",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ProdutoID",
                table: "ShoppingCart",
                column: "ProdutoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DetalhesEncomendas");

            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Encomendas");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Funcionarios");
        }
    }
}
