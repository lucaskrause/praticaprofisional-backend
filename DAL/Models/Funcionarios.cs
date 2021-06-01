using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Funcionarios : Pessoas
    {
        [Required]
        public string cpf { get; set; }

        [Required]
        public string rg { get; set; }

        [Required]
        public int codigoEmpresa { get; set; }

        [JsonIgnore]
        public Empresas empresa { get; set; }

        public string nomeEmpresa { set { empresa ??= new Empresas(); empresa.razaoSocial = value; } }

        public decimal salario { get; set; }

        public DateTime dtAdmissao { get; set; }

        public DateTime dtDemissao { get; set; }
    }
}