using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class FuncionariosDTO
    {
        public FuncionariosDTO() : base()
        {
        }

        [Required]
        public string nome { get; set; }

        [Required]
        public string cpf { get; set; }

        [Required]
        public string rg { get; set; }

        [Required]
        public string sexo { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string telefone { get; set; }

        [Required]
        public DateTime dtNascimento { get; set; }

        [Required]
        public int codigoCidade { get; set; }

        [Required]
        public string logradouro { get; set; }

        [Required]
        public string complemento { get; set; }

        [Required]
        public string bairro { get; set; }

        [Required]
        public string cep { get; set; }

        [Required]
        public int codigoEmpresa { get; set; }

        public decimal salario { get; set; }

        public DateTime dtAdmissao { get; set; }

        public DateTime dtDemissao { get; set; }

        public Funcionarios ToFuncionario()
        {
            return new Funcionarios()
            {
                nome = this.nome,
                cpf = this.cpf,
                rg = this.rg,
                sexo = this.sexo,
                email = this.email,
                telefone = this.telefone,
                dtNascimento = this.dtNascimento,
                codigoCidade = this.codigoCidade,
                logradouro = this.logradouro,
                complemento = this.complemento,
                bairro = this.bairro,
                cep = this.cep,
                codigoEmpresa = this.codigoEmpresa,
                salario = this.salario,
                dtAdmissao = this.dtAdmissao,
                dtDemissao = this.dtDemissao,
            };
        }

        public Funcionarios ToFuncionario(int codigo)
        {
            return new Funcionarios()
            {
                codigo = codigo,
                nome = this.nome,
                cpf = this.cpf,
                rg = this.rg,
                sexo = this.sexo,
                email = this.email,
                telefone = this.telefone,
                dtNascimento = this.dtNascimento,
                codigoCidade = this.codigoCidade,
                logradouro = this.logradouro,
                complemento = this.complemento,
                bairro = this.bairro,
                cep = this.cep,
                codigoEmpresa = this.codigoEmpresa,
                salario = this.salario,
                dtAdmissao = this.dtAdmissao,
                dtDemissao = this.dtDemissao,
            };
        }
    }
}
