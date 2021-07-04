using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class AreasLocacao : AbstractEntity
    {   
        public string descricao { get; set; }
        
        public decimal valor { get; set; }

        public override string Validation()
        {
            if (this.descricao == null || this.descricao == "")
            {
                return "Área de Locação obrigatória";
            }
            else if (this.descricao.Length > 50)
            {
                return "Área de Locação deve ter no máximo 50 caracteres";
            }
            else if (this.valor <= Convert.ToDecimal(0.00) || this.valor > Convert.ToDecimal(99999999.99))
            {
                return "Valor deve ser entre 0.01 e 99,999,999.99";
            }
            else
            {
                return null;
            }
        }
    }
}
