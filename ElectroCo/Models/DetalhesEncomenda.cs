using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroCo.Models
{
    /// <summary>
    /// Representa os dados de um Item(produto) de uma 'encomenda'
    /// </summary>
    public class DetalhesEncomenda
    {

        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Quantidade do produto
        /// </summary>
        public int Quantidade { get; set; }

        /// <summary>
        /// Preço total do Produto
        /// </summary>
        public float PrecoProduto { get; set; }


        //FK para Encomendas
        [ForeignKey(nameof(Order))]
        public int EncomendaID { get; set; }// DetalheEncomenda ---> Encomenda
        public virtual Encomendas Order { get; set; }

        //FK para Produto
        [ForeignKey(nameof(Product))]
        public int ProdutoID { get; set; }// DetalheEncomenda ---> Produto
        public virtual Produtos Product { get; set; }
    }
}
