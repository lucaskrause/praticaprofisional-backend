using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Reservas : AbstractEntity
    {
        [Required]
        public int codigoEmpresa { get; set; }

        [JsonIgnore]
        public Empresas empresa { get; set; }

        [Required]
        public int codigoCliente { get; set; }

        [JsonIgnore]
        public Clientes cliente { get; set; }

        public string nomeCliente { set { cliente ??= new Clientes(); cliente.nome = value; } }

        [Required]
        public int codigoPreco { get; set; }

        [JsonIgnore]
        public Precificacoes preco { get; set; }

        public decimal valor { set { preco ??= new Precificacoes(); preco.valor = value; } }

        [Required]
        public int qtdePessoas { get; set; }

        [Required]
        public DateTime DtReserva { get; set; }
    }
}
