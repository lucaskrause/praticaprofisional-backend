using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Produtos : AbstractEntity
    {
        [Required]
        public string produto { get; set; }

        [Required]
        public int unidades { get; set; }

        [Required]
        public decimal valorCusto { get; set; }

        [Required]
        public int estoque { get; set; }

        [Required]
        public int codigoCategoria { get; set; }

        public Categorias categoria { get; set; }

        public string nomeCategoria { set { categoria ??= new Categorias(); categoria.descricao = value; } }

        public DateTime? dtUltimaCompra { get; set; }

        public decimal? valorUltimaCompra { get; set; }
    }
}
