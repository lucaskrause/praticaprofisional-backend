using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class CotasDTO
    {
        public CotasDTO() : base()
        {
        }

        [Required]
        public int codigoCliente { get; set; }

        [Required]
        public decimal valor { get; set; }

        [Required]
        public DateTime dtInicio { get; set; }

        [Required]
        public DateTime dtTermino { get; set; }

        [Required]
        public int codigoEmpresa { get; set; }

        public Cotas ToCota()
        {
            return new Cotas()
            {
                codigoCliente = this.codigoCliente,
                valor = this.valor,
                dtInicio = this.dtInicio,
                dtTermino = this.dtTermino,
                codigoEmpresa = this.codigoEmpresa
            };
        }

        public Cotas ToCota(int codigo)
        {
            return new Cotas()
            {
                codigo = codigo,
                codigoCliente = this.codigoCliente,
                valor = this.valor,
                dtInicio = this.dtInicio,
                dtTermino = this.dtTermino,
                codigoEmpresa = this.codigoEmpresa
            };
        }
    }
}
