using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class FormasPagamentoDTO
    {
        public FormasPagamentoDTO() : base()
        {
        }

        [Required]
        public string descricao { get; set; }

        public FormasPagamento ToFormaPagamento()
        {
            return new FormasPagamento()
            {
                descricao = this.descricao,
            };
        }

        public FormasPagamento ToFormaPagamento(int codigo)
        {
            return new FormasPagamento()
            {
                codigo = codigo,
                descricao = this.descricao,
            };
        }
    }
}
