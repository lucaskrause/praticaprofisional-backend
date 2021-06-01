using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class ContasBancariasDTO
    {
        public ContasBancariasDTO() : base()
        {
        }

        [Required]
        public string instituicao { get; set; }

        [Required]
        public string numeroBanco { get; set; }

        [Required]
        public string conta { get; set; }

        [Required]
        public string agencia { get; set; }

        public decimal saldo { get; set; }

        [Required]
        public int codigoEmpresa { get; set; }

        public ContasBancarias ToContaBancaria()
        {
            return new ContasBancarias()
            {
                instituicao = this.instituicao,
                numeroBanco = this.numeroBanco,
                conta = this.conta,
                agencia = this.agencia,
                saldo = this.saldo,
                codigoEmpresa = this.codigoEmpresa,
            };
        }

        public ContasBancarias ToContaBancaria(int codigo)
        {
            return new ContasBancarias()
            {
                codigo = codigo,
                instituicao = this.instituicao,
                numeroBanco = this.numeroBanco,
                conta = this.conta,
                agencia = this.agencia,
                saldo = this.saldo,
                codigoEmpresa = this.codigoEmpresa,
            };
        }
    }
}
