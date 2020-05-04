using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroCo.Models
{
    public class Utilizadores
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório")]
        [StringLength(40, ErrorMessage = "O {0} não pode ter mais de {1} carateres.")]
        [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,3}",
                          ErrorMessage = "Deve escrever entre 2 e 4 nomes, começados por uma Maiúscula, seguidos de minúsculas.")]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public virtual ICollection<Cliente> Clients { get; set; }
        public virtual ICollection<Funcionario> Employees { get; set; }
    }
}
