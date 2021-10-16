using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Compras : AbstractEntity
    {
        public string modelo { get; set; }
        public string serie { get; set; }
        public string numeroNF { get; set; }
        public int codigoFornecedor { get; set; }
        public Fornecedores fornecedor { get; set; }
        public string nomeFornecedor { set { fornecedor ??= new Fornecedores(); fornecedor.nome = value; } }
        public int codigoCondicaoPagamento { get; set; }
        public CondicoesPagamento condicaoPagamento { get; set; }
        public string nomeCondicao { set { condicaoPagamento ??= new CondicoesPagamento(); condicaoPagamento.descricao = value; } }
        public DateTime? dtEmissao { get; set; }
        public DateTime? dtEntrega { get; set; }
        public List<ItensCompra> itens { get; set; }

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
