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
    }
}
