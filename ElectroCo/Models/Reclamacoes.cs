using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroCo.Models
{
    public class Reclamacoes
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Assunto da Reclamação
        /// </summary>
        public string Assunto{ get; set; }
        
        /// <summary>
        /// Descrição da Reclamação
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Data da realização da Reclamação
        /// </summary>
        public DateTime DataReclamacao { get; set; }

        /// <summary>
        /// Estado da Reclamação
        /// </summary>
        public string EstadoReclamacao { get; set; }

        //FK para cliente
        [ForeignKey(nameof(Cliente))]
        public int ClientID { get; set; }//Reclamação ---> Cliente
        public virtual ICollection<Clientes> Cliente { get; set; }

        [ForeignKey(nameof(Admin))]
        public int AdminID { get; set; }//Reclamação ---> Funcionário
        public virtual ICollection<Funcionarios> Admin { get; set; }
    }
}
