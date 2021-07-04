using DAL.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Dependentes : Pessoas
    {
        public string cpf { get; set; }

        public string rg { get; set; }

        public int codigoCliente { get; set; }

        public Clientes cliente { get; set; }

        public string nomeCliente { set { cliente ??= new Clientes(); cliente.nome = value; } }

        public override string Validation()
        {
            string error = base.Validation();
            if(error == null)
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
