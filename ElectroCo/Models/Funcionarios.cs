using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectroCo.Models
{
    public class Funcionarios
    {
        public Funcionarios()
        {
            Orders = new HashSet<Encomendas>();
        }

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório")]
        public string Name { get; set; }

        /// <summary>
        /// Email Principal do Utilizador
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Contacto Telefonico de contacto com o Utilizador
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// Número do Funcionário
        /// </summary>
        public int NumFuncionario { get; set; }

        /// <summary>
        /// Tipo do Funcionário (Admin ou Gestor de Armazém)
        /// </summary>
        public string TipoFuncionario { get; set; }

        /// <summary>
        /// Referência ao Utilizador que se autentica
        /// </summary>
        public string UserId { get; set; }

        public virtual ICollection<Encomendas> Orders { get; set; }
    }
}
