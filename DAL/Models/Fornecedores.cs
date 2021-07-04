using DAL.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Fornecedores : Pessoas
    {
        public string tipoPessoa { get; set; }

        public string cpfCnpj { get; set; }

        public string rgIe { get; set; }

        public override string Validation()
        {
            string error = base.Validation();
            if (error == null)
            {
                if (this.tipoPessoa == null || this.tipoPessoa == "")
                {
                    return "Tipo Pessoa obrigatório";
                }
                else if (this.cpfCnpj == null || this.cpfCnpj == "")
                {
                    if (this.tipoPessoa == "PF")
                    {
                        return "CPF obrigatório";
                    }
                    else
                    {
                        return "CNPJ obrigatório";
                    }
                }
                else if(this.dtNascimento == null || this.dtNascimento > DateTime.Now)
                {
                    if (this.tipoPessoa == "PF")
                    {
                        return "Data de Nascimento obrigatória";
                    }
                    else
                    {
                        return "Data de Fundação obrigatória";
                    }
                }
                else if(this.tipoPessoa == "PF")
                {
                    if (!Validadores.validadorCPF(this.cpfCnpj))
                    {
                        return "CPF inválido";
                    }
                    return null;
                }
                else
                {
                    if (!Validadores.validadorCNPJ(this.cpfCnpj))
                    {
                        return "CNPJ inválido";
                    }
                    return null;
                }
            }
            else
            {
                return error;
            }
        }
    }
}
