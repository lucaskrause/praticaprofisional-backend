using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Compras : Pai
    {
        public string modelo { get; set; }

        public string serie { get; set; }

        public string numeroNF { get; set; }

        public int codigoFornecedor { get; set; }

        public Fornecedores fornecedor { get; set; }

        public string nomeFornecedor { set { fornecedor ??= new Fornecedores(); fornecedor.nome = value; } }

        public DateTime dtEmissao { get; set; }

        public DateTime dtEntrega { get; set; }

        public List<ItensCompra> itens { get; set; }

        public decimal valorProdutos { get; set; }

        public decimal frete { get; set; }

        public decimal seguro { get; set; }

        public decimal despesas { get; set; }

        public decimal valorTotal { get; set; }

        public int codigoCondicaoPagamento { get; set; }

        public CondicoesPagamento condicaoPagamento { get; set; }

        public string nomeCondicao { set { condicaoPagamento ??= new CondicoesPagamento(); condicaoPagamento.descricao = value; } }

        public List<ParcelasCompra> parcelas { get; set; }

        public void Cancelar()
        {
            this.status = "Cancelado";
        }

        public override string Validation()
        {
            return null;
        }
    }
}
