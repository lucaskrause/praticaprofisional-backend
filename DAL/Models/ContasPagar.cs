using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ContasPagar : Contas
    {
        public string modelo { get; set; }

        public string serie { get; set; }

        public string numeroNF { get; set; }

        public int codigoFornecedor { get; set; }

        public Fornecedores fornecedor { get; set; }

        public string nomeFornecedor { set { fornecedor ??= new Fornecedores(); fornecedor.nome = value; } }

        public override string Validation()
        {
            return null;
        }
    }
}
