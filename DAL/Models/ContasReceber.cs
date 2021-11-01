using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ContasReceber : Contas
    {
        public int codigoCliente { get; set; }

        public Clientes Cliente { get; set; }

        public string nomeCliente { set { Cliente ??= new Clientes(); Cliente.nome = value; } }

        public virtual void pendente()
        {
            this.status = "Pendente";
        }

        public virtual void pagar()
        {
            this.status = "Pago";
        }

        public override string Validation()
        {
            return null;
        }
    }
}
