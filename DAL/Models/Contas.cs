using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Contas : Pai
    {
        public int numeroParcela { get; set; }

        public double valorParcela { get; set; }

        public int codigoFormaPagamento { get; set; }

        public FormasPagamento formaPagamento { get; set; }

        public string descricaoForma { set { formaPagamento ??= new FormasPagamento(); formaPagamento.descricao = value; } }

        public DateTime? dtVencimento { get; set; }

        public DateTime dtEmissao { get; set; }

        public DateTime? dtPagamento { get; set; }

        public virtual void pendente()
        {
            this.status = "Pendente";
        }

        public virtual void pagar()
        {
            this.status = "Pago";
        }

        public virtual void cancelar()
        {
            this.status = "Cancelado";
        }

        public override string Validation()
        {
            return null;
        }
    }
}
