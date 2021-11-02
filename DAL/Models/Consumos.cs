using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Consumos : Pai
    {
        public int codigoProduto { get; set; }
        public Produtos produto { get; set; }
        public string nomeProduto  { set { produto ??= new Produtos(); produto.produto = value; } }
        public int quantidade { get; set; }
        public int codigoFuncionario { get; set; }
        public Funcionarios funcionario { get; set; }
        public string nomeFuncionario { set { funcionario ??= new Funcionarios(); funcionario.nome = value; } }
        public string observacao { get; set; }

        public override string Validation()
        {
            return null;
        }
    }
}
