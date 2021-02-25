using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Entitys
{
    public class Cidades : AbstractEntity
    {
        public string cidade { get; set; }
        public int estadoId { get; set; }
        public Estados estado { get; set; }
    }
}
