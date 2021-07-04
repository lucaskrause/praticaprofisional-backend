using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class ContasBancarias : AbstractEntity
    {
        public string instituicao { get; set; }

        public string numeroBanco { get; set; }

        public string agencia { get; set; }

        public string conta { get; set; }

        public decimal saldo { get; set; }

        public int codigoEmpresa { get; set; }

        public Empresas empresa { get; set; }

        public override string Validation()
        {
            if (this.instituicao == null || this.instituicao == "")
            {
                return "Instituição obrigatória";
            }
            else if (this.numeroBanco == null || this.numeroBanco == "")
            {
                return "Número do Banco obrigatório";
            }
            else if (this.agencia == null || this.agencia == "")
            {
                return "Agencia obrigatória";
            }
            else if (this.conta == null || this.conta == "")
            {
                return "Número da Conta obrigatório";
            }
            else
            {
                return null;
            }
        }
    }
}
