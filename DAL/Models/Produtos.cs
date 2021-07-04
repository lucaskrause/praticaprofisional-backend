using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Produtos : AbstractEntity
    {
        public string produto { get; set; }

        public int unidades { get; set; }

        public decimal valorCusto { get; set; }

        public int estoque { get; set; }

        public int codigoCategoria { get; set; }

        public Categorias categoria { get; set; }

        public string nomeCategoria { set { categoria ??= new Categorias(); categoria.descricao = value; } }

        public DateTime? dtUltimaCompra { get; set; }

        public decimal? valorUltimaCompra { get; set; }

        public override string Validation()
        {
            if (this.produto == null || this.produto == "")
            {
                return "Produto obrigatório";
            }
            else if (this.unidades < 1)
            {
                return "Unidades obrigatória";
            }
            else if (this.valorCusto <= 0)
            {
                return "Valor de Custo obrigatório";
            }
            else if (this.codigoCategoria <= 0)
            {
                return "Categoria obrigatória";
            }
            else
            {
                return null;
            }
        }
    }
}
