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
        [Key]
        public int ID{ get; set; }

        /// <summary>
        /// Estado da Encomenda (estará em aberta quando o cliente)
        /// O estado será alterado pelo Gestor de Armazém
        /// </summary>
        public string EstadoEncomenda { get; set; }

        /// <summary>
        /// Comprovativo de Compra
        /// </summary>
        public string Comprovativo { get; set; }

        /// <summary>
        /// Data da realização da Encomenda
        /// </summary>
        public DateTime DataEncomenda { get; set; }

        /// <summary>
        /// Morada para onde será enviada a encomenda
        /// </summary>
        public string MoradaEncomenda { get; set; }

        /// <summary>
        /// Morada para onde será faturada a encomenda
        /// </summary>
        public string MoradaFaturacao { get; set; }

        /// <summary>
        /// Data de previsão de entrega da Encomenda
        /// </summary>
        public DateTime PrevisaoEntrega { get; set; }

        /// <summary>
        /// Código/ identificador para seguir a encomenda
        /// </summary>
        public string TrackID { get; set; }

        //FK para Cliente
        [ForeignKey(nameof(Cliente))]
        public int ClientID { get; set; } //Encomenda ---> Cliente
        public virtual ICollection<Clientes> Cliente { get; set; }

        //FK para Funcionario
        [ForeignKey(nameof(Gestor))]
        public int GestorID { get; set; } //Encomenda ---> Funcionário
        public virtual ICollection<Funcionarios> Gestor { get; set; }
    }
}
