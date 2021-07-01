using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Clientes : Pessoas
    {
        public string tipoPessoa { get; set; }

        public string cpfCnpj { get; set; }

        public string rgIe { get; set; }

        public int codigoCondicaoPagamento{ get; set; }

        public CondicoesPagamento condicaoPagamento { get; set; }

        public string nomeCondicao { set { condicaoPagamento ??= new CondicoesPagamento(); condicaoPagamento.descricao = value; } }

        public List<Dependentes> dependentes { get; set; }

        public bool isSocio { get; set; }
    }
}