using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Cotas : AbstractEntity
    {
        public int codigoCliente { get; set; }

        public Clientes cliente { get; set; }

        public string nomeCliente { set { cliente ??= new Clientes(); cliente.nome = value; } }

        public decimal valor { get; set; }

        public DateTime dtInicio { get; set; }

        public DateTime dtTermino { get; set; }

        public int codigoEmpresa { get; set; }
    }
}
