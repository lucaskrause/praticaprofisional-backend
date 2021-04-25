using Newtonsoft.Json;
using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Entities
{
    public class Estados : AbstractEntity
    {
        public Estados()
        {
            pais = new Paises();
        }

        [Required]
        public string estado { get; set; }

        [Required]
        public string uf { get; set; }

        [Required]
        public int codigoPais { get; set; }

        [JsonIgnore]
        public Paises pais { get; set; }

        public string nomePais { set { pais.pais = value; } }

    }
}
