using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroCo.Models
{
    public class Utilizadores
    {
        [Key]
        public int ID { get; set; }
        
        /// <summary>
        ///Nome do Utilizador
        /// </summary>
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

        /*
        /// <summary>
        /// Password Encriptada do Utilizador
        /// </summary>
        ///public string Password { get; set; }
        */

        public virtual ICollection<Clientes> Clients { get; set; }
        public virtual ICollection<Funcionarios> Employees { get; set; }
    }
}
