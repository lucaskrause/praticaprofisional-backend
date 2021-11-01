using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ContasReceberDAO : DAO<ContasReceber>
    {
        public async Task<bool> CheckExist(NpgsqlConnection conexao, string table, string modelo, string serie, string numeroNF, int codigoCliente, int numeroParcela)
        {
            string sql = @"SELECT * FROM contasreceber WHERE contasreceber.modelo = @modelo AND contasreceber.serie = @serie AND contasreceber.numeroNF = @numeroNF AND contasreceber.codigoCliente = @codigoCliente AND contasreceber.numeroParcela = @numeroParcela;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@modelo", modelo);
            command.Parameters.AddWithValue("@serie", serie);
            command.Parameters.AddWithValue("@numeroNF", numeroNF);
            command.Parameters.AddWithValue("@codigoCliente", codigoCliente);
            command.Parameters.AddWithValue("@numeroParcela", numeroParcela);

            List<ContasReceber> list = await GetResultSet(command);

            if (list.Count > 0)
            {
                return false;
            }
            return true;
        }

        public async override Task<IList<ContasReceber>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT contasreceber.modelo, contasreceber.serie, contasreceber.numeronf, contasreceber.codigocliente, contasreceber.numeroparcela, contasreceber.valorparcela, contasreceber.codigoformapagamento, contasreceber.dtemissao, contasreceber.dtvencimento, contasreceber.dtpagamento, contasreceber.status, clientes.nome as nomeCliente, formaspagamento.descricao as descricaoForma FROM contasreceber INNER JOIN clientes ON clientes.codigo = contasreceber.codigocliente INNER JOIN formaspagamento ON formaspagamento.codigo = contasreceber.codigoFormaPagamento;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<ContasReceber> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<ContasReceber> BuscarPorID(int codigo)
        {
            throw new NotImplementedException();
        }

        public async Task<ContasReceber> BuscarParcela(ContasReceber contaReceber)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT contasreceber.modelo, contasreceber.serie, contasreceber.numeronf, contasreceber.codigocliente, contasreceber.numeroparcela, contasreceber.valorparcela, contasreceber.codigoformapagamento, contasreceber.dtemissao, contasreceber.dtvencimento, contasreceber.dtpagamento, contasreceber.status, clientes.nome as nomeCliente, formaspagamento.descricao as descricaoForma FROM contasreceber INNER JOIN clientes ON clientes.codigo = contasreceber.codigocliente INNER JOIN formaspagamento ON formaspagamento.codigo = contasreceber.codigoformapagamento WHERE contasreceber.modelo = @modelo AND contasreceber.serie = @serie AND contasreceber.numeroNF = @numeroNF AND contasreceber.codigoCliente = @codigoCliente AND contasreceber.numeroParcela = @numeroParcela;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@modelo", contaReceber.modelo);
                    command.Parameters.AddWithValue("@serie", contaReceber.serie);
                    command.Parameters.AddWithValue("@numeroNF", contaReceber.numeroNF);
                    command.Parameters.AddWithValue("@codigoCliente", contaReceber.codigoCliente);
                    command.Parameters.AddWithValue("@numeroParcela", contaReceber.numeroParcela);

                    List<ContasReceber> list = await GetResultSet(command);

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

        public async override Task<ContasReceber> Inserir(ContasReceber contaReceber)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                bool exists = await CheckExist(conexao, "contasreceber", contaReceber.modelo, contaReceber.serie, contaReceber.numeroNF, contaReceber.codigoCliente, contaReceber.numeroParcela);
                if (exists)
                {
                    try
                    {
                        string sql = @"INSERT INTO contasreceber(modelo, serie, numeronf, codigocliente, numeroparcela, valorparcela, codigoformapagamento, dtemissao, dtvencimento, dtpagamento, status) VALUES (@modelo, @serie, @numeronf, @codigoCliente, @numeroParcela, @valorParcela, @codigoFormaPagamento, @dtEmissao, @dtVencimento, @dtPagamento, @status);";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@modelo", contaReceber.modelo);
                        command.Parameters.AddWithValue("@serie", contaReceber.serie);
                        command.Parameters.AddWithValue("@numeronf", contaReceber.numeroNF);
                        command.Parameters.AddWithValue("@codigoCliente", contaReceber.codigoCliente);
                        command.Parameters.AddWithValue("@numeroParcela", contaReceber.numeroParcela);
                        command.Parameters.AddWithValue("@valorParcela", contaReceber.valorParcela);
                        command.Parameters.AddWithValue("@codigoFormaPagamento", contaReceber.codigoFormaPagamento);
                        command.Parameters.AddWithValue("@dtEmissao", contaReceber.dtEmissao);
                        command.Parameters.AddWithValue("@dtVencimento", contaReceber.dtVencimento);
                        command.Parameters.AddWithValue("@dtPagamento", contaReceber.dtPagamento ?? (Object)DBNull.Value);
                        command.Parameters.AddWithValue("@status", contaReceber.status);

                        await command.ExecuteNonQueryAsync();

                        return contaReceber;
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

        public async override Task<ContasReceber> Editar(ContasReceber contaReceber)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE contasreceber SET codigoformapagamento = @codigoFormaPagamento, dtvencimento = @dtVencimento WHERE modelo = @modelo AND serie = @serie AND numeronf = @numeronf AND codigocliente = @codigoCliente AND numeroparcela = @numeroParcela;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigoFormaPagamento", contaReceber.codigoFormaPagamento);
                    command.Parameters.AddWithValue("@dtVencimento", contaReceber.dtVencimento);
                    command.Parameters.AddWithValue("@modelo", contaReceber.modelo);
                    command.Parameters.AddWithValue("@serie", contaReceber.serie);
                    command.Parameters.AddWithValue("@numeronf", contaReceber.numeroNF);
                    command.Parameters.AddWithValue("@codigoCliente", contaReceber.codigoCliente);
                    command.Parameters.AddWithValue("@numeroParcela", contaReceber.numeroParcela);

                    await command.ExecuteNonQueryAsync();

                    return contaReceber;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public async Task<ContasReceber> Receber(ContasReceber contaReceber)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE contasreceber SET dtPagamento = @dtPagamento, status = @status WHERE modelo = @modelo AND serie = @serie AND numeronf = @numeronf AND codigocliente = @codigoCliente AND numeroparcela = @numeroParcela;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@dtPagamento", contaReceber.dtPagamento);
                    command.Parameters.AddWithValue("@status", contaReceber.status);
                    command.Parameters.AddWithValue("@modelo", contaReceber.modelo);
                    command.Parameters.AddWithValue("@serie", contaReceber.serie);
                    command.Parameters.AddWithValue("@numeronf", contaReceber.numeroNF);
                    command.Parameters.AddWithValue("@codigoCliente", contaReceber.codigoCliente);
                    command.Parameters.AddWithValue("@numeroParcela", contaReceber.numeroParcela);

                    await command.ExecuteNonQueryAsync();

                    return contaReceber;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<bool> Excluir(ContasReceber entity)
        {
            throw new NotImplementedException();
        }

        public override Task<IList<ContasReceber>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
