using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class EmpresasDTO
    {
        public EmpresasDTO() : base()
        {
        }

        [Required]
        public string razaoSocial { get; set; }

        public string nomeFantasia { get; set; }

        [Required]
        public string cnpj { get; set; }

        public string ie { get; set; }

        [Required]
        public string telefone { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public DateTime dtFundacao { get; set; }

        public int qtdeCotas { get; set; }

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

        public List<ContasBancarias>? contasBancarias { get; set; }

        public Empresas ToEmpresa()
        {
            return new Empresas()
            {
                razaoSocial = this.razaoSocial,
                nomeFantasia = this.nomeFantasia,
                cnpj = this.cnpj,
                ie = this.ie,
                telefone = this.telefone,
                email = this.email,
                dtFundacao = this.dtFundacao,
                qtdeCotas = this.qtdeCotas,
                codigoCidade = this.codigoCidade,
                logradouro = this.logradouro,
                complemento = this.complemento,
                bairro = this.bairro,
                cep = this.cep,
                contasBancarias = this.contasBancarias,
            };
        }

        public Empresas ToEmpresa(int codigo)
        {
            return new Empresas()
            {
                codigo = codigo,
                razaoSocial = this.razaoSocial,
                nomeFantasia = this.nomeFantasia,
                cnpj = this.cnpj,
                ie = this.ie,
                telefone = this.telefone,
                email = this.email,
                dtFundacao = this.dtFundacao,
                qtdeCotas = this.qtdeCotas,
                codigoCidade = this.codigoCidade,
                logradouro = this.logradouro,
                complemento = this.complemento,
                bairro = this.bairro,
                cep = this.cep,
                contasBancarias = this.contasBancarias,
            };
        }
    }
}
