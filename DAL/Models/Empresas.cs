using DAL.Utils;
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

        public string? nomeFantasia { get; set; }

        public string cnpj { get; set; }

        public string? ie { get; set; }

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

        public override string Validation()
        {
            if (this.razaoSocial == null || this.razaoSocial == "")
            {
                return "Razão Social obrigatória";
            }
            else if (this.razaoSocial.Length > 50)
            {
                return "Razão Social deve ter no máximo 50 caracteres";
            }
            else if (this.cnpj == null || this.cnpj == "")
            {
                return "CNPJ obrigatória";
            }
            else if (!Validadores.validadorCNPJ(this.cnpj))
            {
                return "CNPJ inválido";
            }
            else if (this.telefone == null || this.telefone == "")
            {
                return "Telefone obrigatório";
            }
            else if (this.telefone.Length < 14 || this.telefone.Length > 15)
            {
                return "Telefone inválido";
            }
            else if (this.email == null || this.email == "")
            {
                return "Email obrigatório";
            }
            else if (!Validadores.validadorEmail(this.email))
            {
                return "Email inválido";
            }
            else if (this.dtFundacao == null || this.dtFundacao > DateTime.Now)
            {
                return "Data de Fundação obrigatória e deve ser no máximo a data de hoje";
            }
            else if (this.codigoCidade <= 0)
            {
                return "Cidade obrigatória";
            }
            else if (this.logradouro == null || this.logradouro == "")
            {
                return "Logradouro obrigatório";
            }
            else if (this.logradouro.Length > 50)
            {
                return "Logradouro deve ter no máximo 50 caracteres";
            }
            else if (this.bairro == null || this.bairro == "")
            {
                return "Bairro obrigatório";
            }
            else if (this.bairro.Length > 50)
            {
                return "Bairro deve ter no máximo 50 caracteres";
            }
            else if (this.cep == null || this.cep == "")
            {
                return "CEP obrigatório";
            }
            else if (this.cep.Length < 9 || this.cep.Length > 9)
            {
                return "CEP obrigatório";
            }
            else
            {
                if (this.contasBancarias.Count > 0)
                {
                    for (int i = 0; i < this.contasBancarias.Count; i++)
                    {
                        ContasBancarias contaBancaria = this.contasBancarias[i];
                        string error = contaBancaria.Validation();
                        if (error == null)
                        {
                            continue;
                        }
                        else
                        {
                            return error;
                        }
                    }
                    return null;
                }
                return null;
            }
        }
    }
}
