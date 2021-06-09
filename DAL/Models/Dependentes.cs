using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Dependentes : Pessoas
    {
        [Required]
        public string cpf { get; set; }

        [Required]
        public string rg { get; set; }

        [Required]
        public int codigoCliente { get; set; }

        [JsonIgnore]
        public Clientes cliente { get; set; }

        public string nomeCliente { set { cliente ??= new Clientes(); cliente.nome = value; } }
    }
}
