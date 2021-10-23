using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ContasReceber : Contas
    {
        public int codigoCliente { get; set; }

        public Clientes Cliente { get; set; }

        public override string Validation()
        {
            return null;
        }
    }
}
