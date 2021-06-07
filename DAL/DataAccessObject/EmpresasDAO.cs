using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class EmpresasDAO : DAO<Empresas>
    {
        public override async Task<IList<Empresas>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection()) {
                try
                {
                    string sql = @"SELECT empresas.codigo, empresas.razaosocial, empresas.nomefantasia, empresas.cnpj, empresas.ie, empresas.telefone, empresas.email, empresas.dtfundacao, empresas.qtdecotas, empresas.codigocidade, cidades.cidade, empresas.logradouro, empresas.complemento, empresas.bairro, empresas.cep, empresas.dtcadastro, empresas.dtalteracao, empresas.status FROM empresas INNER JOIN cidades ON empresas.codigocidade = cidades.codigo WHERE empresas.status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Empresas> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Empresas> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT empresas.codigo, empresas.razaosocial, empresas.nomefantasia, empresas.cnpj, empresas.ie, empresas.telefone, empresas.email, empresas.dtfundacao, empresas.qtdecotas, empresas.codigocidade, cidades.cidade, empresas.logradouro, empresas.complemento, empresas.bairro, empresas.cep, empresas.dtcadastro, empresas.dtalteracao, empresas.status, cidades.cidade AS nomeCidade FROM empresas INNER JOIN cidades ON empresas.codigocidade = cidades.codigo WHERE empresas.codigo = @codigo AND empresas.status = 'Ativo';";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Empresas> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Empresas> Inserir(Empresas empresa)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();

                try
                {
                    string sql = @"INSERT INTO empresas(razaosocial, nomefantasia, cnpj, ie, telefone, email, dtfundacao, qtdecotas, codigocidade, logradouro, complemento, bairro, cep, dtcadastro, dtalteracao, status) VALUES (@razaoSocial, @nomeFantasia, @cnpj, @ie, @telefone, @email, @dtFundacao, @qtdeCotas, @codigoCidade, @logradouro, @complemento, @bairro, @cep, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@razaoSocial", empresa.razaoSocial);
                    command.Parameters.AddWithValue("@nomeFantasia", empresa.nomeFantasia);
                    command.Parameters.AddWithValue("@cnpj", empresa.cnpj);
                    command.Parameters.AddWithValue("@ie", empresa.ie);
                    command.Parameters.AddWithValue("@telefone", empresa.telefone);
                    command.Parameters.AddWithValue("@email", empresa.email);
                    command.Parameters.AddWithValue("@dtFundacao", empresa.dtFundacao);
                    command.Parameters.AddWithValue("@qtdeCotas", empresa.qtdeCotas);
                    command.Parameters.AddWithValue("@codigoCidade", empresa.codigoCidade);
                    command.Parameters.AddWithValue("@logradouro", empresa.logradouro);
                    command.Parameters.AddWithValue("@complemento", empresa.complemento);
                    command.Parameters.AddWithValue("@bairro", empresa.bairro);
                    command.Parameters.AddWithValue("@cep", empresa.cep);
                    command.Parameters.AddWithValue("@dtCadastro", empresa.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", empresa.dtAlteracao);
                    command.Parameters.AddWithValue("@status", empresa.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    empresa.codigo = (int)idInserido;

                    int qtdContas = empresa.contasBancarias.Count;
                    if (qtdContas > 0)
                    {
                        for (int i = 0; i < qtdContas; i++)
                        {
                            ContasBancarias contaBancaria = empresa.contasBancarias[i];
                            contaBancaria.codigoEmpresa = empresa.codigo;
                            contaBancaria.Ativar();
                            contaBancaria.PrepareSave();

                            sql = @"INSERT INTO contasbancarias(instituicao, numerobanco, agencia, conta, saldo, codigoempresa, dtcadastro, dtalteracao, status) VALUES (@instituicao, @numeroBanco, @agencia, @conta, @saldo, @codigoEmpresa, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                            command = new NpgsqlCommand(sql, conexao);

                            command.Parameters.AddWithValue("@instituicao", contaBancaria.instituicao);
                            command.Parameters.AddWithValue("@numerobanco", contaBancaria.numeroBanco);
                            command.Parameters.AddWithValue("@agencia", contaBancaria.agencia);
                            command.Parameters.AddWithValue("@conta", contaBancaria.conta);
                            command.Parameters.AddWithValue("@saldo", contaBancaria.saldo);
                            command.Parameters.AddWithValue("@codigoempresa", contaBancaria.codigoEmpresa);
                            command.Parameters.AddWithValue("@dtCadastro", contaBancaria.dtCadastro);
                            command.Parameters.AddWithValue("@dtAlteracao", contaBancaria.dtAlteracao);
                            command.Parameters.AddWithValue("@status", contaBancaria.status);

                            idInserido = await command.ExecuteScalarAsync();
                            contaBancaria.codigo = (int)idInserido;
                            empresa.contasBancarias[i] = contaBancaria;
                        }
                    }

                    transaction.Commit();
                    return empresa;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Empresas> Editar(Empresas empresa)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE empresas SET razaosocial = @razaoSocial, nomefantasia = @nomeFantasia, cnpj = @cnpj, ie = @ie, telefone = @telefone, email = @email, dtfundacao = @dtFundacao, qtdecotas = @qtdeCotas, codigocidade = @codigoCidade, logradouro = @logradouro, complemento = @complemento, bairro = @bairro, cep = @cep, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", empresa.codigo);
                    command.Parameters.AddWithValue("@razaoSocial", empresa.razaoSocial);
                    command.Parameters.AddWithValue("@nomeFantasia", empresa.nomeFantasia);
                    command.Parameters.AddWithValue("@cnpj", empresa.cnpj);
                    command.Parameters.AddWithValue("@ie", empresa.ie);
                    command.Parameters.AddWithValue("@telefone", empresa.telefone);
                    command.Parameters.AddWithValue("@email", empresa.email);
                    command.Parameters.AddWithValue("@dtFundacao", empresa.dtFundacao);
                    command.Parameters.AddWithValue("@qtdeCotas", empresa.qtdeCotas);
                    command.Parameters.AddWithValue("@codigoCidade", empresa.codigoCidade);
                    command.Parameters.AddWithValue("@logradouro", empresa.logradouro);
                    command.Parameters.AddWithValue("@complemento", empresa.complemento);
                    command.Parameters.AddWithValue("@bairro", empresa.bairro);
                    command.Parameters.AddWithValue("@cep", empresa.cep);
                    command.Parameters.AddWithValue("@dtAlteracao", empresa.dtAlteracao);

                    await command.ExecuteNonQueryAsync();
                    return empresa;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Empresas empresa)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE empresas SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM empresa WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", empresa.status);
                    command.Parameters.AddWithValue("@dtAlteracao", empresa.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", empresa.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<Empresas>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
