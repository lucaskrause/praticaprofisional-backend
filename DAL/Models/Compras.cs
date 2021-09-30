using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    class Compras : AbstractEntity
    {
        public string modelo { get; set; }
        public string serie { get; set; }
        public string nrNota { get; set; }
        public Fornecedores fornecedor { get; set; }
        public CondicoesPagamento condPagamento { get; set; }
        public DateTime dtEmissao { get; set; }
        public DateTime dtChegada { get; set; }
        public bool cfi { get; set; }
        public double frete { get; set; }
        public double seguro { get; set; }
        public double despesa { get; set; }
        public double totalProduto { get; set; }
        public double totalPagar { get; set; }
        public bool situacao { get; set; }
        public List<ItensCompra> listaItem { get; set; }

        public override string Validation()
        {
            return null;
        }
    }
}
