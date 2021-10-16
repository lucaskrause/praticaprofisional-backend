using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ItensCompra : Produtos
    {
        public string modelo { get; set; }
        public string serie { get; set; }
        public string numeroNF { get; set; }
        public int codigoFornecedor { get; set; }
        public Fornecedores Fornecedor { get; set; }
        public decimal quantidade { get; set; }
        public decimal valorUnitario { get; set; }
        public decimal desconto { get; set; }
        public decimal total { get; set; }
    }
}
