using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Pessoas : AbstractEntity
    {
        public string nome { get; set; }

        public string sexo { get; set; }

        public string email { get; set; }

        public string telefone { get; set; }

        public DateTime dtNascimento { get; set; }

        public int codigoCidade { get; set; }

        public Cidades cidade { get; set; }

        public string nomeCidade { set { cidade ??= new Cidades(); cidade.cidade = value; } }

        public string cep { get; set; }

        public string bairro { get; set; }

        public string logradouro { get; set; }

        public string complemento { get; set; }
    }
}
