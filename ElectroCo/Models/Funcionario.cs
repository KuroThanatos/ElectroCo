using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ElectroCo.Models
{
    public class Funcionario
    {
        [Key]
        public int ID { get; set; }

        public int NumFuncionario { get; set; }

        public string TipoFuncionario { get; set; }

        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }
        public virtual ICollection<Utilizadores> Users { get; set; }
    }
}
