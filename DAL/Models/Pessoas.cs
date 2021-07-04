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

        public override string Validation()
        {
            if (this.nome == null || this.nome == "")
            {
                return "Cliente obrigatório";
            }
            else if (this.telefone == null || this.telefone == "")
            {
                return "Telefone obrigatório";
            }
            else if (this.email == null || this.email == "")
            {
                return "Email obrigatório";
            }
            else if (this.codigoCidade <= 0)
            {
                return "Cidade obrigatória";
            }
            else if (this.logradouro == null || this.logradouro == "")
            {
                return "Logradouro obrigatório";
            }
            else if (this.bairro == null || this.bairro == "")
            {
                return "Bairro obrigatório";
            }
            else if (this.cep == null || this.cep == "")
            {
                return "CEP obrigatório";
            }
            else
            {
                return null;
            }
        }
    }
}
