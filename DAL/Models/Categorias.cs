using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Categorias : AbstractEntity
    {
        [Required]
        public string descricao { get; set; }
    }
}
