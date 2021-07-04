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

        public override string Validation()
        {
            if (this.numeroParcela < 0)
            {
                return "Número da parcela precisa ser maior que 0";
            }
            else if (this.numeroDias < 0)
            {
                return "Número de dias precisa ser maior que 0";
            }
            else if (this.porcentagem <= 0)
            {
                return "Porcentagem é obrigatória em todas as parcelas";
            }
            else if (this.codigoFormaPagamento <= 0)
            {
                return "Forma de Pagamento é obrigatória em todas as parcelas";
            }
            else
            {
                return null;
            }
        }
    }
}
