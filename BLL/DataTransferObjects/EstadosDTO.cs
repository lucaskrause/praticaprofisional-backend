using DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BLL.DataTransferObjects
{
    public class EstadosDTO
    {
        public EstadosDTO() : base()
        {
        }

        [Required]
        public string estado { get; set; }

        [Required]
        public string uf { get; set; }

        [Required]
        public int codigoPais { get; set; }

        public Estados ToEstado()
        {
            return new Estados()
            {
                estado = this.estado,
                uf = this.uf,
                codigoPais = this.codigoPais,
            };
        }

        public Estados ToEstado(int codigo)
        {
            return new Estados()
            {
                codigo = codigo,
                estado = this.estado,
                uf = this.uf,
                codigoPais = this.codigoPais,
            };
        }
    }
}
