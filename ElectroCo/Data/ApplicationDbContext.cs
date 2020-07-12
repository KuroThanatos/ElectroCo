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
        }


        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Produtos> Produtos { get; set; }
        public virtual DbSet<Encomendas> Encomendas { get; set; }
        public virtual DbSet<Funcionarios> Funcionarios { get; set; }
        public virtual DbSet<DetalhesEncomenda> DetalhesEncomendas { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }
    }
 


}
