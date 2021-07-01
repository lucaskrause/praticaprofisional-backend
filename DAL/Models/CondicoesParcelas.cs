using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class CondicoesParcelas : AbstractEntity
    {
        public int codigoCondicaoPagamento { get; set; }

        public int numeroParcela { get; set; }

        public int numeroDias { get; set; }

        public decimal porcentagem { get; set; }

        public int codigoFormaPagamento { get; set; }

        public FormasPagamento formaPagamento { get; set; }

        public string descricaoForma { set { formaPagamento ??= new FormasPagamento(); formaPagamento.descricao = value; formaPagamento.codigo = codigoFormaPagamento; } }
    }
}
