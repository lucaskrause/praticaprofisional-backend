using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Precificacoes : AbstractEntity
    {
        public int minPessoas { get; set; }

        public int maxPessoas { get; set; }

        public decimal valor { get; set; }

        public override string Validation()
        {
            if (this.minPessoas <= 0)
            {
                return "Mínimo de Pessoas obrigatório";
            }
            else if (this.maxPessoas <= 0)
            {
                return "Máximo de Pessoas obrigatório";
            }
            else if (this.valor <= 0)
            {
                return "Valor obrigatório";
            }
            else
            {
                return null;
            }
        }
    }
}
