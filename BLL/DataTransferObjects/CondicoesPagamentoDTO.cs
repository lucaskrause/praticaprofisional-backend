using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class CondicoesPagamentoDTO
    {
        public CondicoesPagamentoDTO() : base()
        {
        }

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

        public CondicoesPagamento ToCondicaoPagamento()
        {
            return new CondicoesPagamento()
            {
                totalParcelas = this.totalParcelas,
                descricao = this.descricao,
                multa = this.multa,
                juros = this.juros,
                desconto = this.desconto,
                parcelas = this.parcelas,
            };
        }

        public CondicoesPagamento ToCondicaoPagamento(int codigo)
        {
            return new CondicoesPagamento()
            {
                codigo = codigo,
                totalParcelas = this.totalParcelas,
                descricao = this.descricao,
                multa = this.multa,
                juros = this.juros,
                desconto = this.desconto,
                parcelas = this.parcelas,
            };
        }
    }
}
