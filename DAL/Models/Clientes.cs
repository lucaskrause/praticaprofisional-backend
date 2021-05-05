using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Clientes : AbstractEntity
    {
        [Required]
        public string nome { get; set; }

        [Required]
        public string tipoPessoa { get; set; }

        [Required]
        public string cpfCnpj { get; set; }

        [Required]
        public string rgIe { get; set; }

        [Required]
        public string sexo { get; set; }

        [Required]
        public DateTime dtNascFundacao { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string telefone { get; set; }

        [Required]
        public int codigoCidade { get; set; }

        [JsonIgnore]
        public Cidades cidade { get; set; }

        public string nomeCidade { set { cidade ??= new Cidades();  cidade.cidade = value; } }

        [Required]
        public string cep { get; set; }

        [Required]
        public string bairro { get; set; }

        [Required]
        public string complemento { get; set; }

        [Required]
        public string logradouro { get; set; }

        [Required]
        public int numero { get; set; }

        [Required]
        public string tipoCliente { get; set; }

        [Required]
        public int codigoFormaPagamento{ get; set; }

        [JsonIgnore]
        public FormasPagamento formaPagamento { get; set; }

        public string nomeForma { set { formaPagamento ??= new FormasPagamento(); formaPagamento.descricao = value; } }
    }
}