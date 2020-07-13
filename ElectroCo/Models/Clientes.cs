using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectroCo.Models
{
    public class Clientes
    {
        public Clientes()
        {
            Orders = new HashSet<Encomendas>();
            Cart = new HashSet<ShoppingCart>();
        }

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório")]
        public string Name { get; set; }

        /// <summary>
        /// Email Principal do Utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Email { get; set; }

        /// <summary>
        /// Contacto Telefonico de contacto com o Utilizador
        /// </summary>
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Deve escrever exatamente {1} algarismos no {0}.")]
        [RegularExpression("[239][0-9]{8}", ErrorMessage = "Deve escrever um nº, com 9 algarismos, começando por 2, 3 ou 9.")]
        public string Telefone { get; set; }

        /// <summary>
        /// Número de Identificação Fiscal, vulgo 'nº de contribuinte'
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")] // o parâmetro {0} representa o 'nome do atributo'
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Deve escrever exatamente {1} algarismos no {0}.")]
        [RegularExpression("[12567][0-9]{8}", ErrorMessage = "Deve escrever um nº, com 9 algarismos, começando por 1, 2, 5, 6 ou 7.")]
        public string NIF { get; set; }

        public string Morada { get; set; }

        public string CodigoPostal { get; set; }

        /// <summary>
        /// Referencia ao Utilizador que se autentica
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// lista de Encomendas de um determinado cliente
        /// </summary>
        public virtual ICollection<Encomendas> Orders { get; set; }

        public virtual ICollection<ShoppingCart> Cart { get; set; }

    }
}