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
            else if (this.valor <= 0)
            {
                return "Valor obrigatório";
            }
            else
            {
                return null;
            }
        }
    }
}
