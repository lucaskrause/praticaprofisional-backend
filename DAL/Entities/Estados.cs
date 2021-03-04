using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RUPsystem.Entities
{
    public class Estados : AbstractEntity
    {
        [Required]
        public string Estado { get; set; }

        [Required]
        public string Uf { get; set; }

        [Required]
        public int CodigoPais { get; set; }

        [JsonIgnore]
        public Paises Pais { get; set; }
    }
}
