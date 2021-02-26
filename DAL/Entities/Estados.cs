using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Entities
{
    public class Estados : AbstractEntity
    {
        public string Estado { get; set; }

        public string Uf { get; set; }

        public int PaisId { get; set; }

        public Paises Pais { get; set; }
    }
}
