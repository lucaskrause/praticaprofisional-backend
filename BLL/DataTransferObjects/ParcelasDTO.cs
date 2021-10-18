using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class ParcelasDTO
    {
        public int codigoCondicaoPagamento { get; set; }
        public decimal valorTotal { get; set; }
        public DateTime dtEmissao { get; set; }
    }
}
