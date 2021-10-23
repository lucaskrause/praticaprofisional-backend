using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ContasPagar : Contas
    {
        public int codigoFornecedor { get; set; }

        public Fornecedores fornecedor { get; set; }

        public string nomeFornecedor { set { fornecedor ??= new Fornecedores(); fornecedor.nome = value; } }

        public override string Validation()
        {
            return null;
        }
    }
}
