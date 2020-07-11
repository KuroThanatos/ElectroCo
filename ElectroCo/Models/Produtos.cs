using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectroCo.Models
{
    public class Produtos
    {
        public Produtos()
        {
            Orders = new HashSet<DetalhesEncomenda>();
        }

        [Key]
        public int ID{ get; set; }

        /// <summary>
        /// Nome do Produto
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Tipo de Produto (exemplo: Placa Gráfica, Memórias, etc...)
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Preço atual do Produto
        /// </summary>
        public float Preco { get; set; }

        /// <summary>
        /// Quantidade Disponível do Produto
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Estado do Produto (ex: Disponível, Indisponível, ...)
        /// </summary>
        public string EstadoProduto { get; set; }

        /// <summary>
        /// Imagem do Produto
        /// </summary>
        public string Imagem { get; set; }

        public virtual ICollection<DetalhesEncomenda> Orders { get; set; }

    }
}