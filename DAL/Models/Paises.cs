using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Paises : AbstractEntity
    {
        [Required]
        public string pais { get; set; }

        [Required]
        public string sigla { get; set; }

        [Required]
        public string ddi { get; set; }
    }
}
