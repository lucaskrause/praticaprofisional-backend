using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Cidades : AbstractEntity
    {
        public string cidade { get; set; }

        public string ddd { get; set; }

        public int codigoEstado { get; set; }

        public Estados estado { get; set; }

        public string nomeEstado { set { estado ??= new Estados(); estado.estado = value; } }

        public string nomeUF { set { estado ??= new Estados(); estado.uf = value; } }
    }
}
