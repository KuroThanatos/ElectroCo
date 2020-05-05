using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ElectroCo.Models
{
    public class Clientes
    {
        public Clientes(){
            Orders = new HashSet<Encomendas>();
        }

        [Key]
        public int ID{ get; set; }

        /// <summary>
        /// Número de Identificação Fiscal, vulgo 'nº de contribuinte'
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")] // o parâmetro {0} representa o 'nome do atributo'
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Deve escrever exatamente {1} algarismos no {0}.")]
        [RegularExpression("[12567][0-9]{8}", ErrorMessage = "Deve escrever um nº, com 9 algarismos, começando por 1, 2, 5, 6 ou 7.")]
        public int NIF { get; set; }
        
        //FK para Utilizadores
        [ForeignKey(nameof(Users))]
        public int UserId { get; set; } //Cliente ---> Utilizador
        public virtual ICollection<Utilizadores> Users { get; set; }

        /// <summary>
        /// lista de Encomendas de um determinado cliente
        /// </summary>
        public virtual ICollection<Encomendas> Orders { get; set; }

    }
}