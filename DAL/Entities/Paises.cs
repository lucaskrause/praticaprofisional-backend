using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Entities
{
    public class Paises : AbstractEntity
    {
        [Required]
        public string Pais { get; set; }

        [Required]
        public string Sigla { get; set; }
    }
}
