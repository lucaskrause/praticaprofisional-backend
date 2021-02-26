using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Entitys
{
    public class Cidades : AbstractEntity
    {
        public string Cidade { get; set; }

        public int EstadoId { get; set; }

        public Estados Estado { get; set; }
    }
}
