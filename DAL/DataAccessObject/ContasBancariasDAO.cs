using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ContasBancariasDAO : DAO<ContasBancarias>
    {
        public async Task<IList<ContasBancarias>> BuscarPorEmpresa(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT codigo, instituicao, numerobanco, agencia, conta, saldo, codigoempresa, dtcadastro, dtalteracao, status FROM contasbancarias WHERE codigoempresa = @codigo AND status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<ContasBancarias> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<ContasBancarias>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT codigo, instituicao, numerobanco, agencia, conta, saldo, codigoempresa, dtcadastro, dtalteracao, status FROM contasbancarias WHERE status = 'Ativo' ORDER BY codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<ContasBancarias> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<ContasBancarias> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT codigo, instituicao, numerobanco, agencia, conta, saldo, codigoempresa, dtcadastro, dtalteracao, status FROM contasbancarias WHERE codigo = @codigo AND status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<ContasBancarias> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<ContasBancarias> Inserir(ContasBancarias contaBancaria)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    conexao.Open();
                    bool exists = await CheckExist(conexao, "contasbancarias", "numerobanco", contaBancaria.numeroBanco);
                    if (exists)
                    {
                        string sql = @"INSERT INTO contasbancarias(instituicao, numerobanco, agencia, conta, saldo, codigoempresa, dtcadastro, dtalteracao, status) VALUES (@instituicao, @numeroBanco, @agencia, @conta, @saldo, @codigoEmpresa, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                        conexao.Open();

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@instituicao", contaBancaria.instituicao);
                        command.Parameters.AddWithValue("@numerobanco", contaBancaria.numeroBanco);
                        command.Parameters.AddWithValue("@agencia", contaBancaria.agencia);
                        command.Parameters.AddWithValue("@conta", contaBancaria.conta);
                        command.Parameters.AddWithValue("@saldo", contaBancaria.saldo);
                        command.Parameters.AddWithValue("@codigoempresa", contaBancaria.codigoEmpresa);
                        command.Parameters.AddWithValue("@dtCadastro", contaBancaria.dtCadastro);
                        command.Parameters.AddWithValue("@dtAlteracao", contaBancaria.dtAlteracao);
                        command.Parameters.AddWithValue("@status", contaBancaria.status);

                        Object idInserido = await command.ExecuteScalarAsync();
                        contaBancaria.codigo = (int)idInserido;
                        return contaBancaria;
                    }
                    else
                    {
                        throw new Exception("Conta já cadastrada");
                    }
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<ContasBancarias> Editar(ContasBancarias contaBancaria)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE contasbancarias SET instituicao = @instituicao, numerobanco = @numerobanco, agencia = @agencia, conta = @conta, saldo = @saldo, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@instituicao", contaBancaria.instituicao);
                    command.Parameters.AddWithValue("@numerobanco", contaBancaria.numeroBanco);
                    command.Parameters.AddWithValue("@agencia", contaBancaria.agencia);
                    command.Parameters.AddWithValue("@conta", contaBancaria.conta);
                    command.Parameters.AddWithValue("@saldo", contaBancaria.saldo);
                    command.Parameters.AddWithValue("@codigoempresa", contaBancaria.codigoEmpresa);
                    command.Parameters.AddWithValue("@dtAlteracao", contaBancaria.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", contaBancaria.codigo);

                    await command.ExecuteNonQueryAsync();
                    return contaBancaria;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(ContasBancarias contaBancaria)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"DELETE FROM contasBancarias WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", contaBancaria.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<ContasBancarias>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
