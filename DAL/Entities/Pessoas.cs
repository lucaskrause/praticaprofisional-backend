using RUPsystem.Entities;
using RUPsystem.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
    public class Pessoas : AbstractEntity
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string CPF_CNPJ { get; set; }

        [Required]
        public string RG_IE { get; set; }

        [Required]
        public string Sexo { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public int CodigoCidade { get; set; }

        [JsonIgnore]
        public Cidades Cidades { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Logradouro { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public string Tipo { get; set; }
    }
}
