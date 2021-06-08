using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Cotas : AbstractEntity
    {
        [Required]
        public int codigoCliente { get; set; }

        [JsonIgnore]
        public Clientes cliente { get; set; }

        public string nomeCliente { set { cliente ??= new Clientes(); cliente.nome = value; } }

        [Required]
        public decimal valor { get; set; }

        [Required]
        public DateTime dtInicio { get; set; }

        [Required]
        public DateTime dtTermino { get; set; }

        [Required]
        public int codigoEmpresa { get; set; }
    }
}
