using RUPsystem.Entities;
using RUPsystem.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
    public class Clientes : AbstractEntity
    {
        [Required]
        public string nome { get; set; }

        [Required]
        public string cpf_cnpj { get; set; }

        [Required]
        public string rg_ie { get; set; }

        [Required]
        public string sexo { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string telefone { get; set; }

        [Required]
        public int codigoCidade { get; set; }

        [JsonIgnore]
        public Cidades cidade { get; set; }

        public string nomeCidade { set { cidade.cidade = value; } }

        [Required]
        public string cep { get; set; }

        [Required]
        public string bairro { get; set; }

        [Required]
        public string logradouro { get; set; }

        [Required]
        public int numero { get; set; }

        [Required]
        public string tipo { get; set; }

        //[JsonIgnore]
        // public FormasPagamentos formaPagamento { get; set; }

        // public string nomeForma { set { formaPagamento.descricao = value; } }

    }
}