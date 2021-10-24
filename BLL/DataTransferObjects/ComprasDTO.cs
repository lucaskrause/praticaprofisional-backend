using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class ComprasDTO
    {
        public string modelo { get; set; }
        public string serie { get; set; }
        public string numeroNF { get; set; }
        public int codigoFornecedor { get; set; }
    }
}
