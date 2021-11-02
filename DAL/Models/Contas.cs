using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Contas : Pai
    {
        public string modelo { get; set; }

        public string serie { get; set; }

        public string numeroNF { get; set; }

        public int numeroParcela { get; set; }

        public double valorParcela { get; set; }

        public int codigoFormaPagamento { get; set; }

        public FormasPagamento formaPagamento { get; set; }

        public string descricaoForma { set { formaPagamento ??= new FormasPagamento(); formaPagamento.descricao = value; } }

        public DateTime dtVencimento { get; set; }

        public DateTime dtEmissao { get; set; }

        public DateTime? dtPagamento { get; set; }

        public override string Validation()
        {
            return null;
        }
    }
}
