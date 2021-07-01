using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class ContasBancarias : AbstractEntity
    {
        public string instituicao { get; set; }

        public string numeroBanco { get; set; }

        public string agencia { get; set; }

        public string conta { get; set; }

        public decimal saldo { get; set; }

        public int codigoEmpresa { get; set; }

        public Empresas empresa { get; set; }
    }
}
