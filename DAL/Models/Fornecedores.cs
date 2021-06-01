using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Fornecedores : Pessoas
    {
        [Required]
        public string tipoPessoa { get; set; }

        [Required]
        public string cpfcnpj { get; set; }

        [Required]
        public string rgie { get; set; }
    }
}
