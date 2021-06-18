using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class PrecificacoesDTO
    {
        public PrecificacoesDTO() : base()
        {
        }

        [Required]
        public int minPessoas { get; set; }

        [Required]
        public int maxPessoas { get; set; }

        [Required]
        public decimal valor { get; set; }

        public Precificacoes ToPreco()
        {
            return new Precificacoes()
            {
                minPessoas = this.minPessoas,
                maxPessoas = this.maxPessoas,
                valor = this.valor,
            };
        }

        public Precificacoes ToPreco(int codigo)
        {
            return new Precificacoes()
            {
                codigo = codigo,
                minPessoas = this.minPessoas,
                maxPessoas = this.maxPessoas,
                valor = this.valor,
            };
        }
    }
}
