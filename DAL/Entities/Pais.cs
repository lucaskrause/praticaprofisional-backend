using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Entitys
{
    public class Pais : GenericEntity
    {
        public string descricao { get; set; }
        public string Sigla { get; set; }
    }
}
