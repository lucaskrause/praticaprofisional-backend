using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class AreasLocacaoDTO
    {
        public AreasLocacaoDTO() : base()
        {
        }

        [Required]
        public string descricao { get; set; }

        [Required]
        public decimal valor { get; set; }

        public AreasLocacao ToAreaLocacao()
        {
            return new AreasLocacao()
            {
                descricao = this.descricao,
                valor = this.valor
            };
        }

        public AreasLocacao ToAreaLocacao(int codigo)
        {
            return new AreasLocacao()
            {
                codigo = codigo,
                descricao = this.descricao,
                valor = this.valor
            };
        }
    }
}
