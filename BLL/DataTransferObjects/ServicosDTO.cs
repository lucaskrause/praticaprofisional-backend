using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class ServicosDTO
    {
        public ServicosDTO() : base()
        {
        }

        [Required]
        public string descricao { get; set; }

        [Required]
        public decimal valor { get; set; }

        public Servicos toServico()
        {
            return new Servicos()
            {
                descricao = this.descricao,
                valor = this.valor,
            };
        }
        public Servicos toServico(int codigo)
        {
            return new Servicos()
            {
                codigo = codigo,
                descricao = this.descricao,
                valor = this.valor,
            };
        }
    }
}
