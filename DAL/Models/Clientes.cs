using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Clientes : Pessoas
    {
        [Required]
        public string tipoPessoa { get; set; }

        [Required]
        public string cpfCnpj { get; set; }

        [Required]
        public string rgIe { get; set; }

        [Required]
        public int codigoCondicaoPagamento{ get; set; }

        [JsonIgnore]
        public CondicoesPagamento condicaoPagamento { get; set; }

        public string nomeCondicao { set { condicaoPagamento ??= new CondicoesPagamento(); condicaoPagamento.descricao = value; } }
    }
}