using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectroCo.Helpers;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace ElectroCo.Models
{
    /// <summary>
    /// Representa um Item do 'carrinho de compras' do cliente
    /// </summary>
    public class ShoppingCart
    {

        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Quantidade do mesmo produto
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public int Quantidade { get; set; }

        //FK para Cliente
        [ForeignKey(nameof(Cliente))]
        public int ClientID { get; set; } //ShoppingCart ---> Cliente
        public virtual Clientes Cliente { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProdutoID { get; set; }// ShoppingCart ---> Produto
        public virtual Produtos Product { get; set; }
    }
}

