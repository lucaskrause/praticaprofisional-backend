using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class CondicoesPagamento : AbstractEntity
    {
        [Required]
        public int totalParcelas { get; set; }

        [Required]
        public string descricao { get; set; }

        [Required]
        public decimal multa { get; set; }

        [Required]
        public decimal juros { get; set; }

        [Required]
        public decimal desconto { get; set; }

        public List<CondicoesParcelas> parcelas { get; set; }
    }
}
