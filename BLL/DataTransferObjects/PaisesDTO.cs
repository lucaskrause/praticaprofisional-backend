using DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BLL.DataTransferObjects
{
    public class PaisesDTO
    {
        public PaisesDTO() : base()
        {
        }

        [Required]
        public string pais { get; set; }

        [Required]
        public string sigla { get; set; }

        [Required]
        public string ddi { get; set; }

        public Paises ToPais()
        {
            return new Paises()
            {
                pais = this.pais,
                sigla = this.sigla,
                ddi = this.ddi,
            };
        }

        public Paises ToPais(int codigo)
        {
            return new Paises()
            {
                codigo = codigo,
                pais = this.pais,
                sigla = this.sigla,
                ddi = this.ddi,
            };
        }
    }
}
