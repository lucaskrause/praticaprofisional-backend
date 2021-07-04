using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Cotas : AbstractEntity
    {
        public int codigoCliente { get; set; }

        public Clientes cliente { get; set; }

        public string nomeCliente { set { cliente ??= new Clientes(); cliente.nome = value; } }

        public decimal valor { get; set; }

        public DateTime dtInicio { get; set; }

        public DateTime dtTermino { get; set; }

        public int codigoEmpresa { get; set; }

        public override string Validation()
        {
            if (this.codigoCliente <= 0)
            {
                return "Cliente obrigatório";
            }
            else if (this.valor <= 0)
            {
                return "Valor obrigatória";
            }
            else if (this.dtInicio == null || this.dtInicio.Date < (DateTime.Now).Date)
            {
                return "Data de Inicio obrigatória, e deve ser data de hoje ou maior";
            }
            else if (this.dtTermino == null || this.dtTermino <= this.dtInicio)
            {
                return "Data de Termino obrigatória, e deve ser maior que a data de inicio";
            }
            else
            {
                return null;
            }
        }
    }
}
