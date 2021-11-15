using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ItensCompra
    {
        public string modelo { get; set; }
        public string serie { get; set; }
        public string numeroNF { get; set; }
        public int codigoFornecedor { get; set; }
        public int codigoProduto { get; set; }
        public string produto { get; set; }
        public int quantidade { get; set; }
        public decimal valorUnitario { get; set; }
        public decimal desconto { get; set; }
        public decimal valorCusto { get; set; }
        public decimal total { get; set; }
    }
}
