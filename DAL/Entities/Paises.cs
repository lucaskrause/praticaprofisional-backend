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
        public string pais { get; set; }

        [Required]
        public string sigla { get; set; }

        [Required]
        public string ddi { get; set; }
    }
}
