using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RUPsystem.Entitys
{
    public class Cidades : AbstractEntity
    {
        [Required]
        public string Cidade { get; set; }

        [Required]
        public string ddd { get; set; }

        [Required]
        public int CodigoEstado { get; set; }

        [JsonIgnore]
        public Estados Estado { get; set; }
    }
}
