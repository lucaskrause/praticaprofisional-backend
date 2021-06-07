﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class ClientesDTO
    {
        public ClientesDTO() : base()
        {
        }

        [Required]
        public string nome { get; set; }

        [Required]
        public string tipoPessoa { get; set; }

        [Required]
        public string cpfCnpj { get; set; }

        [Required]
        public string rgIe { get; set; }

        [Required]
        public string sexo { get; set; }

        [Required]
        public DateTime dtNascimento { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string telefone { get; set; }

        [Required]
        public int codigoCidade { get; set; }

        [Required]
        public string cep { get; set; }

        [Required]
        public string bairro { get; set; }

        [Required]
        public string complemento { get; set; }

        [Required]
        public string logradouro { get; set; }

        [Required]
        public int codigoCondicaoPagamento { get; set; }

        public Clientes ToCliente()
        {
            return new Clientes()
            {
                nome = this.nome,
                tipoPessoa = this.tipoPessoa,
                cpfCnpj = this.cpfCnpj,
                rgIe = this.rgIe,
                sexo = this.sexo,
                dtNascimento = this.dtNascimento,
                email = this.email,
                telefone = this.telefone,
                codigoCidade = this.codigoCidade,
                cep = this.cep,
                bairro = this.bairro,
                complemento = this.complemento,
                logradouro = this.logradouro,
                codigoCondicaoPagamento = this.codigoCondicaoPagamento,
            };
        }

        public Clientes ToCliente(int codigo)
        {
            return new Clientes()
            {
                codigo = codigo,
                nome = this.nome,
                tipoPessoa = this.tipoPessoa,
                cpfCnpj = this.cpfCnpj,
                rgIe = this.rgIe,
                sexo = this.sexo,
                dtNascimento = this.dtNascimento,
                email = this.email,
                telefone = this.telefone,
                codigoCidade = this.codigoCidade,
                cep = this.cep,
                bairro = this.bairro,
                complemento = this.complemento,
                logradouro = this.logradouro,
                codigoCondicaoPagamento = this.codigoCondicaoPagamento,
            };
        }
    }
}
