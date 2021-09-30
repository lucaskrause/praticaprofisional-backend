using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ItensCompra : Produtos
    {
        public string modelo { get; set; }
        public string serie { get; set; }
        public string nrNota { get; set; }
        public decimal custoUnitario { get; set; }
        public decimal valorUnitario { get; set; }
        public Fornecedores Fornecedor { get; set; }
    }
}
