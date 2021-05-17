using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class ContasBancarias : AbstractEntity
    {
        [Required]
        public int codigoEmpresa { get; set; }

        public Empresas empresa { get; set; }

        [Required]
        public string banco { get; set; }

        [Required]
        public string numeroBanco { get; set; }

        [Required]
        public string agencia { get; set; }

        [Required]
        public string conta { get; set; }

        public decimal saldo { get; set; }
    }
}
