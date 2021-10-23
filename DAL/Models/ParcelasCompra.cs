using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ParcelasCompra
    {
        public ParcelasCompra()
        {
            formaPagamento = new FormasPagamento();
        }

        public int numeroParcela { get; set; }
        public decimal valorParcela { get; set; }
        public int codigoFormaPagamento { get; set; }
        public FormasPagamento formaPagamento { get; set; }
        public string descricaoForma { set { formaPagamento ??= new FormasPagamento(); formaPagamento.descricao = value; formaPagamento.codigo = codigoFormaPagamento; } }
        public DateTime dtEmissao { get; set; }
        public DateTime dtVencimento { get; set; }
        public DateTime? dtPagamento { get; set; }
        public string status { get; set; }

        public virtual void pendente()
        {
            this.status = "Pendente";
        }
    }
}
