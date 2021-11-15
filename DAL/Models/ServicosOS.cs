using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ServicosOS
    {
        public int codigoServico { get; set; }
        public string descricao { get; set; }
        public int quantidade { get; set; }
        public decimal valorUnitario { get; set; }
        public decimal total { get; set; }
    }
}
