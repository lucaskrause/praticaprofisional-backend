using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Precificacoes : AbstractEntity
    {

        [Required]
        public string descricao { get; set; }

        [Required]
        public int QtdePessoas { get; set; }

        [Required]
        public decimal valor { get; set; }

        [Required]
        public string tipo { get; set; }
    }
}
