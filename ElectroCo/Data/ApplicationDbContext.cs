using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ElectroCo.Models;
using Microsoft.AspNetCore.Identity;

namespace ElectroCo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "ad", Name = "administrador", NormalizedName = "administrador" },
                new IdentityRole { Id = "ga", Name = "gestorArmazem", NormalizedName = "gestorArmazem" },
                new IdentityRole { Id = "c", Name = "cliente", NormalizedName = "cliente" }
                );
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser { Id = "f554eee4-e19d-4830-a02c-aabe9f18e8a7", UserName = "gerente@ipt.pt", NormalizedUserName = "GERENTE@IPT.PT", Email = "gerente@ipt.pt", NormalizedEmail = "GERENTE@IPT.PT", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEOwjUR76Lx3fR0i9QH3Noni0nzQTLzJ9a2CM1v+IdBwB6ADWtKRgX4o4Sl8FyBIoqA==", SecurityStamp = "CYQGW2ATI3AOJUO66PHZWTHIPBZRU6NL", ConcurrencyStamp = "bd1c4aa5-aaed-45ff-a6e9-11e8c6888644" },
                new IdentityUser { Id = "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816", UserName = "gestor@ipt.pt",NormalizedUserName = "GESTOR@IPT.PT", Email = "gestor@ipt.pt", NormalizedEmail = "GESTOR@IPT.PT", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEOwjUR76Lx3fR0i9QH3Noni0nzQTLzJ9a2CM1v+IdBwB6ADWtKRgX4o4Sl8FyBIoqA==", SecurityStamp = "CYQGW2ATI3AOJUO66PHZWTHIPBZRU6NL", ConcurrencyStamp = "bd1c4aa5-aaed-45ff-a6e9-11e8c6888644" },
                new IdentityUser { Id = "91b48022-fcca-4aed-8bee-63f2ff93a8c5", UserName = "cliente@ipt.pt",NormalizedUserName = "CLIENTE@IPT.PT", Email = "cliente@ipt.pt", NormalizedEmail = "CLIENTE@IPT.PT", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEOwjUR76Lx3fR0i9QH3Noni0nzQTLzJ9a2CM1v+IdBwB6ADWtKRgX4o4Sl8FyBIoqA==", SecurityStamp = "CYQGW2ATI3AOJUO66PHZWTHIPBZRU6NL", ConcurrencyStamp = "bd1c4aa5-aaed-45ff-a6e9-11e8c6888644" }
                );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId= "f554eee4-e19d-4830-a02c-aabe9f18e8a7", RoleId = "ad" },
                new IdentityUserRole<string> { UserId = "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816", RoleId = "ga" },
                new IdentityUserRole<string> { UserId = "91b48022-fcca-4aed-8bee-63f2ff93a8c5", RoleId = "c" }
                );
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string> { Id = 1, UserId = "f554eee4-e19d-4830-a02c-aabe9f18e8a7", ClaimType = "Nome", ClaimValue = "Gerente Gerente" },
                new IdentityUserClaim<string> { Id = 2, UserId = "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816", ClaimType = "Nome", ClaimValue = "Gestor Gestor" },
                new IdentityUserClaim<string> { Id = 3, UserId = "91b48022-fcca-4aed-8bee-63f2ff93a8c5", ClaimType = "Nome", ClaimValue = "Cliente Cliente" }
                );
            modelBuilder.Entity<Funcionarios>().HasData(
                new Funcionarios { ID = 1, Email = "gerente@ipt.pt", Nome = "Gerente Gerente", NumFuncionario = 666, password = null, Telefone = "987456123", TipoFuncionario = "administrador", UserId = "f554eee4-e19d-4830-a02c-aabe9f18e8a7" },
                new Funcionarios { ID = 2, Email = "gestor@ipt.pt", Nome = "Gestor Gestor", NumFuncionario = 777, password = null, Telefone = "987456123", TipoFuncionario = "gestorArmazem", UserId = "96fc6f49-a2b8-42eb-a63d-edc9e8a7c816" }
                );
            modelBuilder.Entity<Clientes>().HasData(
                    new Clientes { ID = 1, Email = "cliente@ipt.pt", Nome = "Cliente Cliente", CodigoPostal = "2000-070 Almeirim", Morada = "Rua São João da Ribeira, nº59", Telefone = "987456123", NIF = "123456789", UserId = "91b48022-fcca-4aed-8bee-63f2ff93a8c5" }
                );

            modelBuilder.Entity<Produtos>().HasData(
                    new Produtos { ID = 1, Nome = "Msi b550 motherboard", Stock = 4, Preco = 210, EstadoProduto= "Disponível", Tipo = "Motherboard", Imagem = "1.png"},
                    new Produtos { ID = 2, Nome = "Asus zenphone", Stock = 10, Preco = 320, EstadoProduto = "Disponível", Tipo = "Smartphones", Imagem = "2.png" },
                    new Produtos { ID = 3, Nome = "AMD Ryzen 3600", Stock = 3, Preco = 186, EstadoProduto = "Disponível", Tipo = "Processadores", Imagem = "3.png" },
                    new Produtos { ID = 4, Nome = "PSU Seasonic 650W Platinum", Stock = 0, Preco = 112, EstadoProduto = "Indisponível", Tipo = "Fonte de Alimentação", Imagem = "4.png" },
                    new Produtos { ID = 5, Nome = "GSKILL 16GB 3600MHZ Cl18", Stock = 21, Preco = 75, EstadoProduto = "Disponível", Tipo = "Memórias RAM", Imagem = "5.png" },
                    new Produtos { ID = 6, Nome = "SSD Samsung 750 500GB", Stock = 56, Preco = 52, EstadoProduto = "Disponível", Tipo = "Armazenamento Interno", Imagem = "6.png" },
                    new Produtos { ID = 7, Nome = "SSD PNY 500GB", Stock = 34, Preco = 48, EstadoProduto = "Disponível", Tipo = "Armazenamento Interno", Imagem = "7.png" },
                    new Produtos { ID = 8, Nome = "ZOTAC NVIDIA RTX 2070 mini ", Stock = 2, Preco = 450, EstadoProduto = "Disponível", Tipo = "Placa Gráfica", Imagem = "8.png" },
                    new Produtos { ID = 9, Nome = "Corsair Crystal 465X", Stock = 2, Preco = 90, EstadoProduto = "Disponível", Tipo = "Caixas de Computador", Imagem = "9.png" },
                    new Produtos { ID = 10, Nome = "Water Cooler CoolerMaster MasterLiquid ML240L RGB", Stock = 12, Preco = 50, EstadoProduto = "Disponível", Tipo = "Coolers CPU", Imagem = "10.png" },
                    new Produtos { ID = 11, Nome = "Corsair LL120 FAN Pack 3 ", Stock = 4, Preco = 80, EstadoProduto = "Disponível", Tipo = "Ventoinhas", Imagem = "11.png" },
                    new Produtos { ID = 12, Nome = "ASUS B550 TUF motherboard", Stock = 1, Preco = 135, EstadoProduto = "Disponível", Tipo = "Motherboard", Imagem = "12.png" }
                );
        }


        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Produtos> Produtos { get; set; }
        public virtual DbSet<Encomendas> Encomendas { get; set; }
        public virtual DbSet<Funcionarios> Funcionarios { get; set; }
        public virtual DbSet<DetalhesEncomenda> DetalhesEncomendas { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }
    }
 


}
