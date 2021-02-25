using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Entitys
{
    public class Estados : AbstractEntity
    {
        public string estado { get; set; }
        public string uf { get; set; }
        public int paisId { get; set; }
        public Paises pais { get; set; }
    }
}
