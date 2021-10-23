using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ContasPagarDAO : DAO<ContasPagar>
    {
        public async override Task<IList<ContasPagar>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT contaspagar.modelo, contaspagar.serie, contaspagar.numeronf, contaspagar.codigofornecedor, contaspagar.numeroparcela, contaspagar.valorparcela, contaspagar.codigoformapagamento, contaspagar.dtemissao, contaspagar.dtvencimento, contaspagar.dtpagamento, contaspagar.status, fornecedores.nome as nomeFornecedor, formaspagamento.descricao as descricaoForma FROM contaspagar INNER JOIN fornecedores ON fornecedores.codigo = contaspagar.codigofornecedor INNER JOIN formaspagamento ON formaspagamento.codigo = contaspagar.codigoFormaPagamento;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<ContasPagar> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<ContasPagar> BuscarPorID(int codigo)
        {
            throw new NotImplementedException();
        }

        public async Task<ContasPagar> BuscarParcela(ContasPagar contaPagar)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT compras.modelo, compras.serie, compras.numeronf, compras.codigofornecedor, compras.codigocondicaopagamento, compras.dtemissao, compras.dtentrega, compras.dtcadastro, compras.dtalteracao, compras.status, fornecedores.nome as nomeFornecedor, condicoespagamento.descricao as nomeCondicao FROM compras INNER JOIN fornecedores ON fornecedores.codigo = compras.codigofornecedor INNER JOIN condicoespagamento ON condicoespagamento.codigo = compras.codigocondicaopagamento WHERE modelo = @modelo AND serie = @serie AND numeroNF = @numeroNF AND codigoFornecedor = @codigoFornecedor;"; // AND status = 'Ativo'

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@modelo", contaPagar.modelo);
                    command.Parameters.AddWithValue("@serie", contaPagar.serie);
                    command.Parameters.AddWithValue("@numeroNF", contaPagar.numeroNF);
                    command.Parameters.AddWithValue("@codigoFornecedor", contaPagar.codigoFornecedor);
                    command.Parameters.AddWithValue("@numeroNF", contaPagar.numeroNF);

                    List<ContasPagar> list = await GetResultSet(command);

                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        throw new Exception("Conta não encontrada");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<ContasPagar> Inserir(ContasPagar entity)
        {
            throw new NotImplementedException();
        }

        public override Task<ContasPagar> Editar(ContasPagar entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Excluir(ContasPagar entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<ContasPagar>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
