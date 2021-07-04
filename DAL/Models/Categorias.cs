using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Categorias : AbstractEntity
    {
        public string descricao { get; set; }

        public override string Validation()
        {
            if (this.descricao == null || this.descricao == "")
            {
                return "Categoria obrigatória";
            }
            else if (this.descricao.Length > 50)
            {
                return "Categoria deve ter no máximo 50 caracteres";
            }
            else
            {
                return null;
            }
        }
    }
}
