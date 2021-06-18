using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Precificacoes : AbstractEntity
    {
        [Required]
        public int minPessoas { get; set; }

        [Required]
        public int maxPessoas { get; set; }

        [Required]
        public decimal valor { get; set; }
    }
}
