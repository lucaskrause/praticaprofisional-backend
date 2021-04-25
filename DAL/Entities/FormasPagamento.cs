using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RUPsystem.Entities
{
    public class FormasPagamento : AbstractEntity
    {
        [Required]
        public string descricao { get; set; } 
    }
}
