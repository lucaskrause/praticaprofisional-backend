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
        public async Task<bool> CheckExist(NpgsqlConnection conexao, string table, string modelo, string serie, string numeroNF, int codigoFornecedor, int numeroParcela)
        {
            string sql = @"SELECT * FROM contaspagar WHERE contaspagar.modelo = @modelo AND contaspagar.serie = @serie AND contaspagar.numeroNF = @numeroNF AND contaspagar.codigoFornecedor = @codigoFornecedor AND contaspagar.numeroParcela = @numeroParcela;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@modelo", modelo);
            command.Parameters.AddWithValue("@serie", serie);
            command.Parameters.AddWithValue("@numeroNF", numeroNF);
            command.Parameters.AddWithValue("@codigoFornecedor", codigoFornecedor);
            command.Parameters.AddWithValue("@numeroParcela", numeroParcela);

            List<ContasPagar> list = await GetResultSet(command);

            if (list.Count > 0)
            {
                return false;
            }
            return true;
        }

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
                    string sql = @"SELECT contaspagar.modelo, contaspagar.serie, contaspagar.numeronf, contaspagar.codigofornecedor, contaspagar.numeroparcela, contaspagar.valorparcela, contaspagar.codigoformapagamento, contaspagar.dtemissao, contaspagar.dtvencimento, contaspagar.dtpagamento, contaspagar.status, fornecedores.nome as nomeFornecedor, formaspagamento.descricao as descricaoForma FROM contaspagar INNER JOIN fornecedores ON fornecedores.codigo = contaspagar.codigofornecedor INNER JOIN formaspagamento ON formaspagamento.codigo = contaspagar.codigoformapagamento WHERE contaspagar.modelo = @modelo AND contaspagar.serie = @serie AND contaspagar.numeroNF = @numeroNF AND contaspagar.codigoFornecedor = @codigoFornecedor AND contaspagar.numeroParcela = @numeroParcela;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@modelo", contaPagar.modelo);
                    command.Parameters.AddWithValue("@serie", contaPagar.serie);
                    command.Parameters.AddWithValue("@numeroNF", contaPagar.numeroNF);
                    command.Parameters.AddWithValue("@codigoFornecedor", contaPagar.codigoFornecedor);
                    command.Parameters.AddWithValue("@numeroParcela", contaPagar.numeroParcela);

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

        public async override Task<ContasPagar> Inserir(ContasPagar contaPagar)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                bool exists = await CheckExist(conexao, "contaspagar", contaPagar.modelo, contaPagar.serie, contaPagar.numeroNF, contaPagar.codigoFornecedor, contaPagar.numeroParcela);
                if (exists)
                {
                    try
                    {
                        string sql = @"INSERT INTO contaspagar(modelo, serie, numeronf, codigofornecedor, numeroparcela, valorparcela, codigoformapagamento, dtemissao, dtvencimento, dtpagamento, status) VALUES (@modelo, @serie, @numeronf, @codigoFornecedor, @numeroParcela, @valorParcela, @codigoFormaPagamento, @dtEmissao, @dtVencimento, @dtPagamento, @status);";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@modelo", contaPagar.modelo);
                        command.Parameters.AddWithValue("@serie", contaPagar.serie);
                        command.Parameters.AddWithValue("@numeronf", contaPagar.numeroNF);
                        command.Parameters.AddWithValue("@codigoFornecedor", contaPagar.codigoFornecedor);
                        command.Parameters.AddWithValue("@numeroParcela", contaPagar.numeroParcela);
                        command.Parameters.AddWithValue("@valorParcela", contaPagar.valorParcela);
                        command.Parameters.AddWithValue("@codigoFormaPagamento", contaPagar.codigoFormaPagamento);
                        command.Parameters.AddWithValue("@dtEmissao", contaPagar.dtEmissao);
                        command.Parameters.AddWithValue("@dtVencimento", contaPagar.dtVencimento);
                        command.Parameters.AddWithValue("@dtPagamento", contaPagar.dtPagamento ?? (Object)DBNull.Value);
                        command.Parameters.AddWithValue("@status", contaPagar.status);

                        await command.ExecuteNonQueryAsync();

                        return contaPagar;
                    }
                    finally
                    {
                        conexao.Close();
                    }
                }
                else
                {
                    throw new Exception("Conta já cadastrada");
                }
            }
        }

        public async override Task<ContasPagar> Editar(ContasPagar contaPagar)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE contaspagar SET codigoformapagamento = @codigoFormaPagamento, dtvencimento = @dtVencimento WHERE modelo = @modelo AND serie = @serie AND numeronf = @numeronf AND codigofornecedor = @codigoFornecedor AND numeroparcela = @numeroParcela;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigoFormaPagamento", contaPagar.codigoFormaPagamento);
                    command.Parameters.AddWithValue("@dtVencimento", contaPagar.dtVencimento);
                    command.Parameters.AddWithValue("@modelo", contaPagar.modelo);
                    command.Parameters.AddWithValue("@serie", contaPagar.serie);
                    command.Parameters.AddWithValue("@numeronf", contaPagar.numeroNF);
                    command.Parameters.AddWithValue("@codigoFornecedor", contaPagar.codigoFornecedor);
                    command.Parameters.AddWithValue("@numeroParcela", contaPagar.numeroParcela);

                    await command.ExecuteNonQueryAsync();

                    return contaPagar;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public async Task<ContasPagar> Pagar(ContasPagar contaPagar)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE contaspagar SET dtPagamento = @dtPagamento, status = @status WHERE modelo = @modelo AND serie = @serie AND numeronf = @numeronf AND codigofornecedor = @codigoFornecedor AND numeroparcela = @numeroParcela;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@dtPagamento", contaPagar.dtPagamento);
                    command.Parameters.AddWithValue("@status", contaPagar.status);
                    command.Parameters.AddWithValue("@modelo", contaPagar.modelo);
                    command.Parameters.AddWithValue("@serie", contaPagar.serie);
                    command.Parameters.AddWithValue("@numeronf", contaPagar.numeroNF);
                    command.Parameters.AddWithValue("@codigoFornecedor", contaPagar.codigoFornecedor);
                    command.Parameters.AddWithValue("@numeroParcela", contaPagar.numeroParcela);

                    await command.ExecuteNonQueryAsync();

                    return contaPagar;
                }
                finally
                {
                    conexao.Close();
                }
            }
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
