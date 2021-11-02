using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Produtos : Pai
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
            else if (this.produto.Length > 50)
            {
                return "Produto deve ter no máximo 50 caracteres";
            }
            else if (this.unidades < 1)
            {
                return "Unidades deve ser no mínimo 1";
            }
            else if (this.valorCusto <= Convert.ToDecimal(0.00) || this.valorCusto > Convert.ToDecimal(99999999.99))
            {
                return "Valor de custo deve ser entre 0.01 e 99999999.99";
            }
            else if (this.estoque < 1)
            {
                return "Estoque deve ser no mínimo 1";
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
