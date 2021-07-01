using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Fornecedores : Pessoas
    {
        public string tipoPessoa { get; set; }

        public string cpfcnpj { get; set; }

        public string rgie { get; set; }
    }
}
