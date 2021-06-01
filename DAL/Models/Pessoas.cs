using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Pessoas : AbstractEntity
    {
        [Required]
        public string nome { get; set; }

        [Required]
        public string sexo { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string telefone { get; set; }

        [Required]
        public DateTime dtNascimento { get; set; }

        [Required]
        public int codigoCidade { get; set; }

        [JsonIgnore]
        public Cidades cidade { get; set; }

        public string nomeCidade { set { cidade ??= new Cidades(); cidade.cidade = value; } }

        [Required]
        public string cep { get; set; }

        [Required]
        public string bairro { get; set; }

        [Required]
        public string logradouro { get; set; }

        [Required]
        public string complemento { get; set; }
    }
}
