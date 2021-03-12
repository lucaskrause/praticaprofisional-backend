using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
    public class Reservas : AbstractEntity
    {
        [Required]
        public int CodigoPessoa { get; set; }

        [JsonIgnore]
        public Pessoas Pessoas { get; set; }

        [Required]
        public DateTime DtReserva { get; set; }

        [Required]
        public int CodigoPrecificacao { get; set; }

        [JsonIgnore]
        public Precificacoes Precificacoes { get; set; }
    }
}
