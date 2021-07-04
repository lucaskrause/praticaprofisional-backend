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

        public override string Validation()
        {
            if (this.cidade == null || this.cidade == "")
            {
                return "Cidade obrigatória";
            }
            else if (this.ddd == null || this.ddd == "")
            {
                return "DDD obrigatório";
            }
            else if (this.codigoEstado <= 0)
            {
                return "Estado obrigatório";
            }
            else
            {
                return null;
            }
        }
    }
}
