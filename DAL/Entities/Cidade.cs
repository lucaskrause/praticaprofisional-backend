using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Entitys
{
    public class Cidade : GenericEntity
    {
        public string descricao { get; set; }
        public int estadoId { get; set; }
        public Estado estado { get; set; }
    }
}
