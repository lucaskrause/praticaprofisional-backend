using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Reservas : AbstractEntity
    {
        [Required]
        public int codigoEmpresa { get; set; }

        public Empresas empresa { get; set; }

        [Required]
        public int codigoCliente { get; set; }

        public Clientes cliente { get; set; }

        public string nomeCliente { set { cliente ??= new Clientes(); cliente.nome = value; } }

        [Required]
        public int qtdePessoas { get; set; }

        [Required]
        public DateTime dtReserva { get; set; }

        [Required]
        public decimal valor { get; set; }

        public List<AreasLocacao> areasLocacao { get; set; }
    }
}
