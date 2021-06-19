using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class CategoriasDTO
    {
        [Required]
        public string descricao { get; set; }

        public Categorias ToCategoria()
        {
            return new Categorias()
            {
                descricao = this.descricao,
            };
        }

        public Categorias ToCategoria(int codigo)
        {
            return new Categorias()
            {
                codigo = codigo,
                descricao = this.descricao,
            };
        }
    }
}
