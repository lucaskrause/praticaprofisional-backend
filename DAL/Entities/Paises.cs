using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Entities
{
    public class Paises : AbstractEntity
    {
        public string Pais { get; set; }

        public string Sigla { get; set; }
    }
}
