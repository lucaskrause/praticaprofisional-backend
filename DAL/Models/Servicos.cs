using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Servicos : AbstractEntity
    {
        public string descricao { get; set; }

        public decimal valor { get; set; }

        public override string Validation()
        {
            if (this.descricao == null || this.descricao == "")
            {
                return "Serviço obrigatório";
            }
            else if (this.descricao.Length > 50)
            {
                return "Serviço deve ter no máximo 50 caracteres";
            }
            else if (this.valor <= Convert.ToDecimal(0.00) || this.valor > Convert.ToDecimal(99999999.99))
            {
                return "valor deve ser entre 0.01 e 99,999,999.99";
            }
            else
            {
                return null;
            }
        }
    }
}
