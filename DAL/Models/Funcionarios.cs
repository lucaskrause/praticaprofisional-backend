using DAL.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Funcionarios : Pessoas
    {
        public string cpf { get; set; }

        public string rg { get; set; }

        public int codigoEmpresa { get; set; }

        public Empresas empresa { get; set; }

        public string nomeEmpresa { set { empresa ??= new Empresas(); empresa.razaoSocial = value; } }

        public decimal salario { get; set; }

        public DateTime dtAdmissao { get; set; }

        public DateTime dtDemissao { get; set; }

        public override string Validation()
        {
            string error = base.Validation();
            if (error == null)
            {
                if (this.cpf == null || this.cpf == "")
                {
                    return "CPF obrigatório";
                }
                else if (!Validadores.validadorCPF(this.cpf))
                {
                    return "CPF inválido";
                }
                else if (this.dtNascimento == null || this.dtNascimento > DateTime.Now)
                {
                    return "Data de Nascimento obrigatória";
                }
                else if (this.salario <= 0)
                {
                    return "Salário obrigatório";
                }
                else if (this.dtAdmissao == null || this.dtAdmissao.Year > 1900)
                {
                    return "Data de Admissão obrigatória";
                }
                else
                {
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