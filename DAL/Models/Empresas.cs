using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Empresas : AbstractEntity
    {
        [Required]
        public string razaoSocial { get; set; }

        public string nomeFantasia { get; set; }

        [Required]
        public string cnpj { get; set; }

        public string ie { get; set; }

        [Required]
        public string telefone { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public DateTime dtFundacao { get; set; }

        public int qtdeCotas { get; set; }

        [Required]
        public int codigoCidade { get; set; }

        [JsonIgnore]
        public Cidades cidade { get; set; }

        public string nomeCidade { set { cidade ??= new Cidades(); cidade.cidade = value; } }

        [Required]
        public string logradouro { get; set; }

        [Required]
        public string complemento { get; set; }

        [Required]
        public string bairro { get; set; }

        [Required]
        public string cep { get; set; }

        public List<ContasBancarias> contasBancarias { get; set; }
    }
}
