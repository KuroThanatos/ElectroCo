using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ElectroCo.Models
{
    public class Funcionarios
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Número do Funcionário
        /// </summary>
        public int NumFuncionario { get; set; }

        /// <summary>
        /// Tipo do Funcionário (Admin ou Gestor de Armazém)
        /// </summary>
        public string TipoFuncionario { get; set; }

        //FK para Utilizador
        [ForeignKey(nameof(Users))]
        public int UserId { get; set; } //Funcionario ---> Utilizador
        public virtual ICollection<Utilizadores> Users { get; set; }
    }
}
