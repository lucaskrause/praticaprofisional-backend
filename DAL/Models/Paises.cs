using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Paises : AbstractEntity
    {
        public string pais { get; set; }

        public string sigla { get; set; }

        public string ddi { get; set; }
    }
}
