using Newtonsoft.Json;
using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Entitys
{
    public class Cidades : AbstractEntity
    {
        public Cidades()
        {
            estado = new Estados();
        }

        [Required]
        public string cidade { get; set; }

        [Required]
        public string ddd { get; set; }

        [Required]
        public int codigoEstado { get; set; }

        [JsonIgnore]
        public Estados estado { get; set; }

        public string nomeEstado { set { estado.estado = value; } }
    }
}
