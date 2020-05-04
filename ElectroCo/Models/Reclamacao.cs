using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroCo.Models
{
    public class Reclamacao
    {
        [Key]
        public int ID { get; set; }

        public string Assunto{ get; set; }

        public string Descricao { get; set; }

        public DateTime DataReclamacao { get; set; }

        public string EstadoReclamacao { get; set; }

        [ForeignKey(nameof(Cliente))]
        public int ClientID { get; set; }
        public virtual ICollection<Cliente> Cliente { get; set; }

        [ForeignKey(nameof(Admin))]
        public int AdminID { get; set; }
        public virtual ICollection<Funcionario> Admin { get; set; }
    }
}
