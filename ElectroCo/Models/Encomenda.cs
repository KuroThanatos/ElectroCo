using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroCo.Models
{
    public class Encomenda
    {  
        [Key]
        public int ID{ get; set; }

        public string EstadoEncomenda { get; set; }

        public DateTime DataEncomenda { get; set; }

        public DateTime PrevisaoEntrega { get; set; }

        public string TrackID { get; set; }

        [ForeignKey(nameof(Cliente))]
        public int ClientID { get; set; }
        public virtual ICollection<Cliente> Cliente { get; set; }

        [ForeignKey(nameof(Gestor))]
        public int GestorID { get; set; }
        public virtual ICollection<Funcionario> Gestor { get; set; }
    }
}
