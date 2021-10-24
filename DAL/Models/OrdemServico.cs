using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class OrdemServico : AbstractEntity
    {
        public DateTime? dtAbertura { get; set; }

        public DateTime? dtValidade { get; set; }

        public DateTime? dtExecucao { get; set; }

        public CondicoesPagamento condicaoPagamento { get; set; }

        public Servicos servico { get; set; }

        public decimal? vlTotal { get; set; }

        public string observacao { get; set; }

        public override string Validation()
        {
            return null;
        }
    }
}
