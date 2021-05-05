using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Cidades : AbstractEntity
    {
        [Required]
        public string cidade { get; set; }

        [Required]
        public string ddd { get; set; }

        [Required]
        public int codigoEstado { get; set; }

        [JsonIgnore]
        public Estados estado { get; set; }

        public string nomeEstado { set { estado ??= new Estados(); estado.estado = value; } }
    }
}
