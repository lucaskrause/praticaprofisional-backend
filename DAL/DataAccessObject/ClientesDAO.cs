using DAL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class ClientesDAO : DAO<Clientes>
    {
        public async Task<List<Cotas>> GetCotasResultSet(NpgsqlCommand command)
        {
            List<Cotas> list = new List<Cotas>();

            command.ExecuteNonQuery();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                DataTable schemaTable = reader.GetSchemaTable();

                JTokenWriter writer = new JTokenWriter();
                writer.WriteStartObject();

                foreach (DataRow row in schemaTable.Rows)
                {
                    writer.WritePropertyName(row[0].ToString());
                    writer.WriteValue(reader[row[0].ToString()]);
                }
                writer.WriteEndObject();
                JObject o = (JObject)writer.Token;
                var stringJson = o.ToString();
                Cotas p = JsonConvert.DeserializeObject<Cotas>(stringJson);
                list.Add(p);
            }
            return list;
        }

        public async Task<List<Dependentes>> GetDependentesResultSet(NpgsqlCommand command)
        {
            List<Dependentes> list = new List<Dependentes>();

            command.ExecuteNonQuery();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                DataTable schemaTable = reader.GetSchemaTable();

                JTokenWriter writer = new JTokenWriter();
                writer.WriteStartObject();

                foreach (DataRow row in schemaTable.Rows)
                {
                    writer.WritePropertyName(row[0].ToString());
                    writer.WriteValue(reader[row[0].ToString()]);
                }
                writer.WriteEndObject();
                JObject o = (JObject)writer.Token;
                var stringJson = o.ToString();
                Dependentes p = JsonConvert.DeserializeObject<Dependentes>(stringJson);
                list.Add(p);
            }
            return list;
        }

        public async Task<bool> CheckExistDependente(NpgsqlConnection conexao, string table, string column, string value)
        {
            string sql = @"SELECT * FROM " + table + " WHERE " + column + " = @value;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@value", value);

            List<Dependentes> list = await GetDependentesResultSet(command);

            if (list.Count > 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ClienteIsSocio(NpgsqlConnection conexao, int codigoCliente)
        {
            string sql = @"SELECT * FROM cotas WHERE cotas.codigoCliente = @codigo AND cotas.status = 'Ativo';";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@codigo", codigoCliente);

            List<Cotas> listCotas = await GetCotasResultSet(command);

            if (listCotas.Count > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Dependentes>> GetDependentes(NpgsqlConnection conexao, int codigoCliente)
        {
            string sql = @"SELECT dependentes.codigo, dependentes.nome, dependentes.cpf, dependentes.rg, dependentes.sexo, dependentes.email, dependentes.telefone, dependentes.dtnascimento, dependentes.codigocidade, dependentes.logradouro, dependentes.complemento, dependentes.bairro, dependentes.cep, dependentes.codigocliente, dependentes.dtcadastro, dependentes.dtalteracao, dependentes.status, cidades.cidade as nomeCidade, clientes.nome as nomeCliente FROM dependentes INNER JOIN cidades ON (dependentes.codigoCidade = cidades.codigo) INNER JOIN clientes ON (dependentes.codigoCliente = clientes.codigo) WHERE dependentes.codigocliente = @codigo AND dependentes.status = 'Ativo';";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@codigo", codigoCliente);

            List<Dependentes> listDependentes = await GetDependentesResultSet(command);

            return listDependentes;
        }

        public async Task<Dependentes> InsertDependente(NpgsqlConnection conexao, Dependentes dependente)
        {
            bool exists = await CheckExistDependente(conexao, "clientes", "cpfcnpj", dependente.cpf);
            if (exists)
            {
                string sql = @"INSERT INTO dependentes(nome, cpf, rg, sexo, email, telefone, dtnascimento, codigocidade, logradouro, complemento, bairro, cep, codigocliente, dtcadastro, dtalteracao, status) VALUES (@nome, @cpf, @rg, @sexo, @email, @telefone, @dtNascimento, @codigoCidade, @logradouro, @complemento, @bairro, @cep, @codigoCliente, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                command.Parameters.AddWithValue("@nome", dependente.nome);
                command.Parameters.AddWithValue("@cpf", dependente.cpf);
                command.Parameters.AddWithValue("@rg", dependente.rg);
                command.Parameters.AddWithValue("@sexo", dependente.sexo);
                command.Parameters.AddWithValue("@email", dependente.email);
                command.Parameters.AddWithValue("@telefone", dependente.telefone);
                command.Parameters.AddWithValue("@dtNascimento", dependente.dtNascimento);
                command.Parameters.AddWithValue("@codigoCidade", dependente.codigoCidade);
                command.Parameters.AddWithValue("@logradouro", dependente.logradouro);
                command.Parameters.AddWithValue("@complemento", dependente.complemento);
                command.Parameters.AddWithValue("@bairro", dependente.bairro);
                command.Parameters.AddWithValue("@cep", dependente.cep);
                command.Parameters.AddWithValue("@codigoCliente", dependente.codigoCliente);
                command.Parameters.AddWithValue("@dtCadastro", dependente.dtCadastro);
                command.Parameters.AddWithValue("@dtAlteracao", dependente.dtAlteracao);
                command.Parameters.AddWithValue("@status", dependente.status);

                Object idInserido = await command.ExecuteScalarAsync();
                dependente.codigo = (int)idInserido;

                return dependente;
            }
            else
            {
                throw new Exception("Dependente " + dependente.nome + " já cadastrado");
            }
        }

        public async Task<Dependentes> UpdateDependente(NpgsqlConnection conexao, Dependentes dependente)
        {
            string sql = @"UPDATE dependentes SET nome = @nome, cpf = @cpf, rg = @rg, sexo = @sexo, email = @email, telefone = @telefone, dtnascimento = @dtNascimento, codigocidade = @codigoCidade, logradouro = @logradouro, complemento = @complemento, bairro = @bairro, cep = @cep, codigocliente = @codigoCliente, dtalteracao = @dtAlteracao, status = @status WHERE codigo = @codigo;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@nome", dependente.nome);
            command.Parameters.AddWithValue("@cpf", dependente.cpf);
            command.Parameters.AddWithValue("@rg", dependente.rg);
            command.Parameters.AddWithValue("@sexo", dependente.sexo);
            command.Parameters.AddWithValue("@email", dependente.email);
            command.Parameters.AddWithValue("@telefone", dependente.telefone);
            command.Parameters.AddWithValue("@dtNascimento", dependente.dtNascimento);
            command.Parameters.AddWithValue("@codigoCidade", dependente.codigoCidade);
            command.Parameters.AddWithValue("@logradouro", dependente.logradouro);
            command.Parameters.AddWithValue("@complemento", dependente.complemento);
            command.Parameters.AddWithValue("@bairro", dependente.bairro);
            command.Parameters.AddWithValue("@cep", dependente.cep);
            command.Parameters.AddWithValue("@codigoCliente", dependente.codigoCliente);
            command.Parameters.AddWithValue("@dtAlteracao", dependente.dtAlteracao);
            command.Parameters.AddWithValue("@status", dependente.status);
            command.Parameters.AddWithValue("@codigo", dependente.codigo);

            await command.ExecuteNonQueryAsync();
            return dependente;
        }

        public async Task<bool> DeleteDependente(NpgsqlConnection conexao, int codigoDependente)
        {
            string sql = @"DELETE FROM dependentes WHERE codigo = @codigo;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@codigo", codigoDependente);

            var result = await command.ExecuteNonQueryAsync();
            return result == 1 ? true : false;
        }

        public override async Task<IList<Clientes>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT clientes.codigo, clientes.nome, clientes.tipopessoa, clientes.cpfcnpj, clientes.rgie, clientes.sexo, clientes.email, clientes.telefone, clientes.dtnascfundacao, clientes.codigocidade, clientes.logradouro, clientes.complemento, clientes.bairro, clientes.cep, clientes.codigocondicaopagamento, clientes.dtcadastro, clientes.dtalteracao, clientes.status, cidades.cidade as nomeCidade, condicoespagamento.descricao AS nomeCondicao FROM clientes INNER JOIN cidades ON (clientes.codigoCidade = cidades.codigo) INNER JOIN condicoespagamento ON (clientes.codigocondicaopagamento = condicoespagamento.codigo) WHERE clientes.status = 'Ativo' ORDER BY clientes.codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Clientes> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }
        public override async Task<Clientes> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"SELECT clientes.codigo, clientes.nome, clientes.tipopessoa, clientes.cpfcnpj, clientes.rgie, clientes.sexo, clientes.email, clientes.telefone, clientes.dtnascfundacao AS dtNascimento, clientes.codigocidade, clientes.logradouro, clientes.complemento, clientes.bairro, clientes.cep, clientes.codigocondicaopagamento, clientes.dtcadastro, clientes.dtalteracao, clientes.status, cidades.cidade as nomeCidade, condicoespagamento.descricao AS nomeCondicao FROM clientes INNER JOIN cidades ON (clientes.codigoCidade = cidades.codigo) INNER JOIN condicoespagamento ON (clientes.codigocondicaopagamento = condicoespagamento.codigo) WHERE clientes.codigo = @codigo AND clientes.status = 'Ativo';";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Clientes> list = await GetResultSet(command);

                    if (list.Count > 0)
                    {
                        list[0].dependentes = await GetDependentes(conexao, codigo);

                        list[0].isSocio = await ClienteIsSocio(conexao, codigo);
                    }

                    transaction.Commit();
                    return list[0];
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

        public async Task<Clientes> BuscarSocioPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"SELECT clientes.codigo, clientes.nome, clientes.tipopessoa, clientes.cpfcnpj, clientes.rgie, clientes.sexo, clientes.email, clientes.telefone, clientes.dtnascfundacao, clientes.codigocidade, clientes.logradouro, clientes.complemento, clientes.bairro, clientes.cep, clientes.codigocondicaopagamento, clientes.dtcadastro, clientes.dtalteracao, clientes.status, cidades.cidade as nomeCidade, condicoespagamento.descricao AS nomeCondicao FROM clientes INNER JOIN cidades ON (clientes.codigoCidade = cidades.codigo) INNER JOIN condicoespagamento ON (clientes.codigocondicaopagamento = condicoespagamento.codigo) WHERE clientes.codigo = @codigo AND clientes.status = 'Ativo' AND EXISTS(SELECT codigoCliente FROM cotas WHERE codigoCliente = clientes.codigo AND status = 'Ativo');";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Clientes> list = await GetResultSet(command);

                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Clientes> Inserir(Clientes cliente)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    bool exists = await CheckExist(conexao, "clientes", "cpfcnpj", cliente.cpfCnpj);
                    if (exists)
                    {
                        string sql = @"INSERT INTO clientes(nome, tipopessoa, cpfcnpj, rgie, sexo, email, telefone, dtnascfundacao, codigocidade, logradouro, complemento, bairro, cep, codigocondicaopagamento, dtcadastro, dtalteracao, status) VALUES (@nome, @tipoPessoa, @cpfcnpj, @rgie, @sexo, @email, @telefone, @dtnascfundacao, @codigoCidade, @logradouro, @complemento, @bairro, @cep, @codigoCondicaoPagamento, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@nome", cliente.nome);
                        command.Parameters.AddWithValue("@tipoPessoa", cliente.tipoPessoa);
                        command.Parameters.AddWithValue("@cpfcnpj", cliente.cpfCnpj);
                        command.Parameters.AddWithValue("@rgie", cliente.rgIe);
                        command.Parameters.AddWithValue("@sexo", cliente.sexo);
                        command.Parameters.AddWithValue("@email", cliente.email);
                        command.Parameters.AddWithValue("@telefone", cliente.telefone);
                        command.Parameters.AddWithValue("@dtnascfundacao", cliente.dtNascimento);
                        command.Parameters.AddWithValue("@codigoCidade", cliente.codigoCidade);
                        command.Parameters.AddWithValue("@logradouro", cliente.logradouro);
                        command.Parameters.AddWithValue("@complemento", cliente.complemento);
                        command.Parameters.AddWithValue("@bairro", cliente.bairro);
                        command.Parameters.AddWithValue("@cep", cliente.cep);
                        command.Parameters.AddWithValue("@codigoCondicaoPagamento", cliente.codigoCondicaoPagamento);
                        command.Parameters.AddWithValue("@dtCadastro", cliente.dtCadastro);
                        command.Parameters.AddWithValue("@dtAlteracao", cliente.dtAlteracao);
                        command.Parameters.AddWithValue("@status", cliente.status);

                        Object idInserido = await command.ExecuteScalarAsync();
                        cliente.codigo = (int)idInserido;

                        int qtdDependentes = cliente.dependentes.Count;
                        if (qtdDependentes > 0)
                        {
                            for (int i = 0; i < qtdDependentes; i++)
                            {
                                Dependentes dependente = cliente.dependentes[i];
                                dependente.codigoCliente = cliente.codigo;
                                dependente.PrepareSave();
                                dependente.Ativar();
                                cliente.dependentes[i] = await InsertDependente(conexao, dependente);
                            }
                        }

                        transaction.Commit();
                        return cliente;
                    }
                    else
                    {
                        throw new Exception("Cliente já cadastrado");
                    }
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

        public override async Task<Clientes> Editar(Clientes cliente)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"UPDATE clientes SET codigo = @codigo, nome = @nome, tipopessoa = @tipoPessoa, cpfcnpj = @cpfcnpj, rgie = @rgie, sexo = @sexo, email = @email, telefone = @telefone, dtnascfundacao = @dtNascFundacao, codigocidade = @codigoCidade, logradouro = @logradouro, complemento = @complemento, bairro = @bairro, cep = @cep, codigocondicaopagamento = @codigoCondicaoPagamento, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@nome", cliente.nome);
                    command.Parameters.AddWithValue("@tipoPessoa", cliente.tipoPessoa);
                    command.Parameters.AddWithValue("@cpfcnpj", cliente.cpfCnpj);
                    command.Parameters.AddWithValue("@rgie", cliente.rgIe);
                    command.Parameters.AddWithValue("@sexo", cliente.sexo);
                    command.Parameters.AddWithValue("@email", cliente.email);
                    command.Parameters.AddWithValue("@telefone", cliente.telefone);
                    command.Parameters.AddWithValue("@dtnascfundacao", cliente.dtNascimento);
                    command.Parameters.AddWithValue("@codigoCidade", cliente.codigoCidade);
                    command.Parameters.AddWithValue("@logradouro", cliente.logradouro);
                    command.Parameters.AddWithValue("@complemento", cliente.complemento);
                    command.Parameters.AddWithValue("@bairro", cliente.bairro);
                    command.Parameters.AddWithValue("@cep", cliente.cep);
                    command.Parameters.AddWithValue("@codigoCondicaoPagamento", cliente.codigoCondicaoPagamento);
                    command.Parameters.AddWithValue("@dtAlteracao", cliente.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", cliente.codigo);

                    await command.ExecuteNonQueryAsync();

                    int qtdDependentes = cliente.dependentes.Count;
                    if (qtdDependentes > 0)
                    {
                        for (int i = 0; i < qtdDependentes; i++)
                        {
                            Dependentes dependente = cliente.dependentes[i];
                            dependente.PrepareSave();
                            if (dependente.codigo == 0)
                            {
                                dependente.codigoCliente = cliente.codigo;
                                dependente.Ativar();
                                cliente.dependentes[i] = await InsertDependente(conexao, dependente);
                            }
                            else if (dependente.codigo > 0 && dependente.status == "Ativo")
                            {
                                cliente.dependentes[i] = await UpdateDependente(conexao, dependente);
                            }
                            else
                            {
                                await DeleteDependente(conexao, dependente.codigo);
                                cliente.dependentes.RemoveAt(i);
                            }
                        }
                    }

                    transaction.Commit();
                    return cliente;
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

        public override async Task<bool> Excluir(Clientes cliente)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"DELETE FROM cliente WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", cliente.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                catch
                {
                    string sql = @"UPDATE clientes SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", cliente.status);
                    command.Parameters.AddWithValue("@dtAlteracao", cliente.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", cliente.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override Task<IList<Clientes>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
