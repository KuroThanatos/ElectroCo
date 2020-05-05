using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroCo.Models
{
    public class DetalheEncomenda
    {
        [Key]
        public int ID { get; set; }

        public int Quantidade { get; set; }

        public float PrecoProduto { get; set; }

        [ForeignKey(nameof(Order))]
        public int EncomendaID { get; set; }
        public virtual ICollection<Encomenda> Order { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProdutoID { get; set; }
        public virtual ICollection<Produtos> Product { get; set; }
    }
}
