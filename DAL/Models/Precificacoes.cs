using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Precificacoes : AbstractEntity
    {
        public int minPessoas { get; set; }

        public int maxPessoas { get; set; }

        public decimal valor { get; set; }
    }
}
