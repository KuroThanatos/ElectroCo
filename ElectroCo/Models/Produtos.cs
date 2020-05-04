using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ElectroCo.Models
{
    public class Produtos
    {
        [Key]
        public int ID{ get; set; }

        public string TipoProduto { get; set; }

        public float Preco { get; set; }

        public int Stock { get; set; }

        public string EstadoProduto { get; set; }

    }
}