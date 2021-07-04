using System;
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
            if (this.minPessoas < 0)
            {
                return "Mínimo de Pessoas obrigatório";
            }
            else if (this.minPessoas == 0 || this.minPessoas > 70)
            {
                return "Mínimo de Pessoas deve estar entre 1 e 70";
            }
            else if (this.maxPessoas < 0)
            {
                return "Máximo de Pessoas obrigatório";
            }
            else if (this.maxPessoas < this.minPessoas || this.maxPessoas > 70)
            {
                return "Máximo de Pessoas deve estar entre " + this.minPessoas + " e 70";
            }
            else if (this.valor <= Convert.ToDecimal(0.00) || this.valor > Convert.ToDecimal(99999999.99))
            {
                return "valor deve ser entre 0.01 e 99,999,999.99";
            }
            else
            {
                return null;
            }
        }
    }
}
