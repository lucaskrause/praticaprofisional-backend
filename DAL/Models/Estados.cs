using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Estados : AbstractEntity
    {
        [Required]
        public string estado { get; set; }

        [Required]
        public string uf { get; set; }

        [Required]
        public int codigoPais { get; set; }

        [JsonIgnore]
        public Paises pais { get; set; }

        public string nomePais { set { pais ??= new Paises(); pais.pais = value; } }

    }
}
