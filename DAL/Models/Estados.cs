using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Estados : AbstractEntity
    {
        public string estado { get; set; }

        public string uf { get; set; }

        public int codigoPais { get; set; }

        public Paises pais { get; set; }

        public string nomePais { set { pais ??= new Paises(); pais.pais = value; } }

    }
}
