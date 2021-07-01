using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Empresas : AbstractEntity
    {
        public string razaoSocial { get; set; }

        public string nomeFantasia { get; set; }

        public string cnpj { get; set; }

        public string ie { get; set; }

        public string telefone { get; set; }

        public string email { get; set; }

        public DateTime dtFundacao { get; set; }

        public int qtdeCotas { get; set; }

        public int codigoCidade { get; set; }

        public Cidades cidade { get; set; }

        public string nomeCidade { set { cidade ??= new Cidades(); cidade.cidade = value; } }

        public string logradouro { get; set; }

        public string complemento { get; set; }

        public string bairro { get; set; }

        public string cep { get; set; }

        public List<ContasBancarias> contasBancarias { get; set; }
    }
}
