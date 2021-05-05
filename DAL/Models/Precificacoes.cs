using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Precificacoes : AbstractEntity
    {
        [Required]
        public string descricao { get; set; }

        [Required]
        public int qtdePessoas { get; set; }

        [Required]
        public decimal valor { get; set; }

        [Required]
        public string tipo { get; set; }
    }
}
