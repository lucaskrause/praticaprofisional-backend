using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class ProdutosDTO
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

        public DateTime? dtUltimaCompra { get; set; }

        public decimal? valorUltimaCompra { get; set; }

        public Produtos ToProduto()
        {
            return new Produtos()
            {
                produto = this.produto,
                unidades = this.unidades,
                valorCusto = this.valorCusto,
                estoque = this.estoque,
                codigoCategoria = this.codigoCategoria,
                dtUltimaCompra = this.dtUltimaCompra,
                valorUltimaCompra = valorUltimaCompra,
            };
        }

        public Produtos ToProduto(int codigo)
        {
            return new Produtos()
            {
                codigo = codigo,
                produto = this.produto,
                unidades = this.unidades,
                valorCusto = this.valorCusto,
                estoque = this.estoque,
                codigoCategoria = this.codigoCategoria,
                dtUltimaCompra = this.dtUltimaCompra,
                valorUltimaCompra = valorUltimaCompra,
            };
        }
    }
}
