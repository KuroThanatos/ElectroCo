using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectroCo.Models
{
    public class Produtos
    {
        /// <summary>
        /// Representa os dados de um 'produto'
        /// </summary>
        public Produtos()
        {
            Orders = new HashSet<DetalhesEncomenda>();
            Cart = new HashSet<ShoppingCart>();
        }

        [Key]
        public int ID{ get; set; }

        /// <summary>
        /// Nome do Produto
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(70, ErrorMessage = "Não pode ter maid do que {1} caráteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Tipo de Produto (exemplo: Placa Gráfica, Memórias, etc...)
        /// </summary>

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Tipo { get; set; }

        /// <summary>
        /// Preço atual do Produto
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")] 
        public float Preco { get; set; }

        /// <summary>
        /// Quantidade Disponível do Produto
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public int Stock { get; set; }

        /// <summary>
        /// Estado do Produto (ex: Disponível, Indisponível, ...)
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string EstadoProduto { get; set; }

        /// <summary>
        /// Imagem do Produto
        /// </summary>
        public string Imagem { get; set; }
        /// <summary>
        /// lista de Encomendas de detalhesEncomendas 
        /// </summary>
        public virtual ICollection<DetalhesEncomenda> Orders { get; set; }

        /// <summary>
        /// lista de Encomendas de ShoppingCarts ao qual este pertence 
        /// </summary>
        public virtual ICollection<ShoppingCart> Cart { get; set; }

    }
}