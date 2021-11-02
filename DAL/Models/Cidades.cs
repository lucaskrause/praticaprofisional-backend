using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Cidades : Pai
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
            else if (this.cidade.Length > 50)
            {
                return "Cidade deve ter no máximo 50 caracteres";
            }
            else if (this.ddd == null || this.ddd == "")
            {
                return "DDD obrigatório";
            }
            else if (this.ddd.Length == 1 || this.ddd.Length > 4)
            {
                return "DDD deve ter entre 2 e 4 caracteres";
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
