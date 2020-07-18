using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroCo.Models
{
    public class Encomendas
    {
        /// <summary>
        /// Representa os dados de uma 'Encomenda'
        /// </summary>
        public Encomendas()
        {
            Orders = new HashSet<DetalhesEncomenda>();
        }

        [Key]
        public int ID{ get; set; }

        /// <summary>
        /// Estado da Encomenda (estará em aberta quando o cliente)
        /// O estado será alterado pelo Gestor de Armazém
        /// </summary>
        [Display(Name = "Estado da Encomenda")]
        public string EstadoEncomenda { get; set; }

        /// <summary>
        /// Data da realização da Encomenda
        /// </summary>
        [Display(Name = "Data da Encomenda")]
        public DateTime DataEncomenda { get; set; }

        /// <summary>
        /// Morada para onde será enviada a encomenda
        /// </summary>
        [Display(Name = "Morada de Envio")]
        public string MoradaEncomenda { get; set; }

        /// <summary>
        /// Morada para onde será faturada a encomenda
        /// </summary>
        [Display(Name = "Morada de Faturação")]
        public string MoradaFaturacao { get; set; }

        /// <summary>
        /// Data de previsão de entrega da Encomenda
        /// </summary>
        [Display(Name = "Previsão de Entrega")]
        public DateTime PrevisaoEntrega { get; set; }

        /// <summary>
        /// Código/ identificador para seguir a encomenda
        /// </summary>
        [Display(Name = "TrackID")]
        public string TrackID { get; set; }

        //FK para Cliente
        [ForeignKey(nameof(Cliente))]
        public int ClientID { get; set; } //Encomenda ---> Cliente
        public virtual Clientes Cliente { get; set; }

        //FK para Funcionario
        [ForeignKey(nameof(Gestor))]
        public int GestorID { get; set; } //Encomenda ---> Funcionário
        public virtual Funcionarios Gestor { get; set; }

        public virtual ICollection<DetalhesEncomenda> Orders { get; set; }
    }
}
