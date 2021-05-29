using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class CidadesDTO
    {
        public CidadesDTO() : base()
        {
        }

        [Required]
        public string cidade { get; set; }

        [Required]
        public string ddd { get; set; }

        [Required]
        public int codigoEstado { get; set; }

        public Cidades ToCidade()
        {
            return new Cidades
            {
                cidade = this.cidade,
                ddd = this.ddd,
                codigoEstado = this.codigoEstado,
            };
        }

        public Cidades ToCidade(int codigo)
        {
            return new Cidades
            {
                codigo = codigo,
                cidade = this.cidade,
                ddd = this.ddd,
                codigoEstado = this.codigoEstado,
            };
        }
    }
}
