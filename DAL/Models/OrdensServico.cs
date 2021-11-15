using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class OrdensServico : Pai
    {
        public int codigoFornecedor { get; set; }

        public Fornecedores fornecedor { get; set; }

        public string nomeFornecedor { set { fornecedor ??= new Fornecedores(); fornecedor.nome = value; } }

        public DateTime dtInicial { get; set; }

        public DateTime dtFinal { get; set; }

        public List<ServicosOS> servicos { get; set; }

        public decimal valorServicos { get; set; }

        public List<ItensCompra> itens { get; set; }

        public decimal valorProdutos { get; set; }

        public decimal valorTotal { get; set; }

        public int codigoCondicaoPagamento { get; set; }

        public CondicoesPagamento condicaoPagamento { get; set; }

        public string nomeCondicao { set { condicaoPagamento ??= new CondicoesPagamento(); condicaoPagamento.descricao = value; } }

        public List<ParcelasCompra> parcelas { get; set; }

        public override string Validation()
        {
            return null;
        }
    }
}
