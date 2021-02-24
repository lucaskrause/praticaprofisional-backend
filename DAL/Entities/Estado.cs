using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Entitys
{
    public class Estado : GenericEntity
    {
        public string descricao { get; set; }
        public int paisId { get; set; }
        public Pais pais { get; set; }
    }
}
