using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class CondicoesParcelas : AbstractEntity
    {
        [Required]
        public int codigoCondicaoPagamento { get; set; }

        [Required]
        public int numeroParcela { get; set; }

        [Required]
        public int numeroDias { get; set; }

        [Required]
        public decimal porcentagem { get; set; }

        [Required]
        public int codigoFormaPagamento { get; set; }

        public FormasPagamento formaPagamento { get; set; }

        public string descricaoForma { set { formaPagamento ??= new FormasPagamento(); formaPagamento.descricao = value; formaPagamento.codigo = codigoFormaPagamento; } }
    }
}
