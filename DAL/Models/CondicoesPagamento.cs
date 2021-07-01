using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class CondicoesPagamento : AbstractEntity
    {
        public int totalParcelas { get; set; }

        public string descricao { get; set; }

        public decimal multa { get; set; }

        public decimal juros { get; set; }

        public decimal desconto { get; set; }

        public List<CondicoesParcelas> parcelas { get; set; }
    }
}
