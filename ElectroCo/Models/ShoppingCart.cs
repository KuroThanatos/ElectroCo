using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroCo.Models
{
    public class ShoppingCart
    {

        [Key]
        public int ID { get; set; }


        //FK para Cliente
        [ForeignKey(nameof(Cliente))]
        public int ClientID { get; set; } //ShoppingCart ---> Cliente
        public virtual Clientes Cliente { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProdutoID { get; set; }// ShoppingCart ---> Produto
        public virtual Produtos Product { get; set; }
    }
}

