using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class ReservasDTO
    {
        public ReservasDTO() : base()
        {
        }

        [Required]
        public int codigoEmpresa { get; set; }

        [Required]
        public int codigoCliente { get; set; }

        [Required]
        public int qtdePessoas { get; set; }

        [Required]
        public DateTime dtReserva { get; set; }

        [Required]
        public decimal valor { get; set; }

        [Required]
        public int codigoCondicaoPagamento { get; set; }

        public List<AreasLocacao> areasLocacao { get; set; }

        public Reservas ToReserva()
        {
            return new Reservas()
            {
                codigoEmpresa = this.codigoEmpresa,
                codigoCliente = this.codigoCliente,
                qtdePessoas = this.qtdePessoas,
                dtReserva = this.dtReserva,
                valor = this.valor,
                codigoCondicaoPagamento = this.codigoCondicaoPagamento,
                areasLocacao = this.areasLocacao,
            };
        }

        public Reservas ToReserva(int codigo)
        {
            return new Reservas()
            {
                codigo = codigo,
                codigoEmpresa = this.codigoEmpresa,
                codigoCliente = this.codigoCliente,
                qtdePessoas = this.qtdePessoas,
                dtReserva = this.dtReserva,
                valor = this.valor,
                codigoCondicaoPagamento = this.codigoCondicaoPagamento,
                areasLocacao = this.areasLocacao,
            };
        }
    }
}
