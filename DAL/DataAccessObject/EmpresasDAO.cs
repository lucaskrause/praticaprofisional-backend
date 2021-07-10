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
    public class EmpresasDAO : DAO<Empresas>
    {
        public async Task<List<ContasBancarias>> GetContasBancariasResultSet(NpgsqlCommand command)
        {
            List<ContasBancarias> list = new List<ContasBancarias>();

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
                ContasBancarias p = JsonConvert.DeserializeObject<ContasBancarias>(stringJson);
                list.Add(p);
            }
            return list;
        }

        public async Task<bool> CheckExistContasBancarias(NpgsqlConnection conexao, string table, string column, string value, int codigo = 0)
        {
            string sql = "";
            if (codigo > 0)
            {
                sql = @"SELECT * FROM " + table + " WHERE " + column + " = @value AND codigo != @codigo;";
            }
            else
            {
                sql = @"SELECT * FROM " + table + " WHERE " + column + " = @value;";
            }

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@value", value);
            command.Parameters.AddWithValue("@codigo", codigo);

            List<ContasBancarias> list = await GetContasBancariasResultSet(command);

            if (list.Count > 0)
            {
                return false;
            }
            return true;
        }

        public async Task<List<ContasBancarias>> GetContasBancarias(NpgsqlConnection conexao, int codigoEmpresa)
        {

            string sql = @"SELECT codigo, instituicao, numerobanco, agencia, conta, saldo, codigoempresa, dtcadastro, dtalteracao, status FROM contasbancarias WHERE codigoEmpresa = @codigo AND status = 'Ativo';";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@codigo", codigoEmpresa);

            List<ContasBancarias> listContasBancarias = await GetContasBancariasResultSet(command);

            return listContasBancarias;
        }

        public async Task<ContasBancarias> InsertContaBancaria(NpgsqlConnection conexao, ContasBancarias contaBancaria, int codigoEmpresa)
        {
            bool exists = await CheckExistContasBancarias(conexao, "contasbancarias", "numerobanco", contaBancaria.numeroBanco);
            if (exists)
            {
                contaBancaria.codigoEmpresa = codigoEmpresa;
                string sql = @"INSERT INTO contasbancarias(instituicao, numerobanco, agencia, conta, saldo, codigoempresa, dtcadastro, dtalteracao, status) VALUES (@instituicao, @numeroBanco, @agencia, @conta, @saldo, @codigoEmpresa, @dtCadastro, @dtAlteracao, @status) returning codigo;";

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
            }
            else
            {
                throw new Exception("Conta Bancaria onde o número da conta é " + contaBancaria.numeroBanco + " já está cadastrada");
            }
            return contaBancaria;
        }

        public async Task<ContasBancarias> UpdateContaBancaria(NpgsqlConnection conexao, ContasBancarias contaBancaria)
        {
            bool exists = await CheckExistContasBancarias(conexao, "contasbancarias", "numerobanco", contaBancaria.numeroBanco, contaBancaria.codigo);
            if (exists)
            {
                string sql = @"UPDATE contasbancarias SET instituicao = @instituicao, numerobanco = @numerobanco, agencia = @agencia, conta = @conta, saldo = @saldo, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

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
            }
            else
            {
                throw new Exception("Conta Bancaria onde o número da conta é " + contaBancaria.numeroBanco + " já está cadastrada");
            }
            return contaBancaria;
        }

        public async Task<bool> DeleteContaBancaria(NpgsqlConnection conexao, int codigoContaBancaria)
        {
            string sql = @"DELETE FROM contasbancarias WHERE codigo = @codigo;";

            NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

            command.Parameters.AddWithValue("@codigo", codigoContaBancaria);

            var result = await command.ExecuteNonQueryAsync();
            return result == 1 ? true : false;
        }

        public override async Task<IList<Empresas>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection()) {
                try
                {
                    string sql = @"SELECT empresas.codigo, empresas.razaosocial, empresas.nomefantasia, empresas.cnpj, empresas.ie, empresas.telefone, empresas.email, empresas.dtfundacao, empresas.qtdecotas, empresas.codigocidade, cidades.cidade AS nomeCidade, empresas.logradouro, empresas.complemento, empresas.bairro, empresas.cep, empresas.dtcadastro, empresas.dtalteracao, empresas.status FROM empresas INNER JOIN cidades ON empresas.codigocidade = cidades.codigo WHERE empresas.status = 'Ativo' ORDER BY empresas.codigo;";

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
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    string sql = @"SELECT empresas.codigo, empresas.razaosocial, empresas.nomefantasia, empresas.cnpj, empresas.ie, empresas.telefone, empresas.email, empresas.dtfundacao, empresas.qtdecotas, empresas.codigocidade, cidades.cidade as nomeCidade, empresas.logradouro, empresas.complemento, empresas.bairro, empresas.cep, empresas.dtcadastro, empresas.dtalteracao, empresas.status, cidades.cidade AS nomeCidade FROM empresas INNER JOIN cidades ON empresas.codigocidade = cidades.codigo WHERE empresas.codigo = @codigo AND empresas.status = 'Ativo';";

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Empresas> list = await GetResultSet(command);

                    if (list.Count > 0)
                    {
                        list[0].contasBancarias = await GetContasBancarias(conexao, codigo);

                        transaction.Commit();
                        return list[0];
                    } else
                    {
                        throw new Exception("Empresa não encontrada");
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

        public override async Task<Empresas> Inserir(Empresas empresa)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    bool exists = await CheckExist(conexao, "empresas", "cnpj", empresa.cnpj);
                    if (exists)
                    {
                        string sql = @"INSERT INTO empresas(razaosocial, nomefantasia, cnpj, ie, telefone, email, dtfundacao, qtdecotas, codigocidade, logradouro, complemento, bairro, cep, dtcadastro, dtalteracao, status) VALUES (@razaoSocial, @nomeFantasia, @cnpj, @ie, @telefone, @email, @dtFundacao, @qtdeCotas, @codigoCidade, @logradouro, @complemento, @bairro, @cep, @dtCadastro, @dtAlteracao, @status) returning codigo;";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@razaoSocial", empresa.razaoSocial);
                        command.Parameters.AddWithValue("@nomeFantasia", empresa.nomeFantasia ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@cnpj", empresa.cnpj);
                        command.Parameters.AddWithValue("@ie", empresa.ie ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@telefone", empresa.telefone);
                        command.Parameters.AddWithValue("@email", empresa.email);
                        command.Parameters.AddWithValue("@dtFundacao", empresa.dtFundacao);
                        command.Parameters.AddWithValue("@qtdeCotas", empresa.qtdeCotas);
                        command.Parameters.AddWithValue("@codigoCidade", empresa.codigoCidade);
                        command.Parameters.AddWithValue("@logradouro", empresa.logradouro);
                        command.Parameters.AddWithValue("@complemento", empresa.complemento ?? (object)DBNull.Value);
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
                                contaBancaria.PrepareSave();
                                contaBancaria.Ativar();
                                empresa.contasBancarias[i] = await InsertContaBancaria(conexao, contaBancaria, empresa.codigo);
                            }
                        }

                        transaction.Commit();
                        return empresa;
                    }
                    else
                    {
                        throw new Exception("Empresa já cadastrada");
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

        public override async Task<Empresas> Editar(Empresas empresa)
        {
            using (var conexao = GetCurrentConnection())
            {
                conexao.Open();
                NpgsqlTransaction transaction = conexao.BeginTransaction();
                try
                {
                    bool exists = await CheckExist(conexao, "empresas", "cnpj", empresa.cnpj, empresa.codigo);
                    if (exists)
                    {
                        string sql = @"UPDATE empresas SET razaosocial = @razaoSocial, nomefantasia = @nomeFantasia, cnpj = @cnpj, ie = @ie, telefone = @telefone, email = @email, dtfundacao = @dtFundacao, qtdecotas = @qtdeCotas, codigocidade = @codigoCidade, logradouro = @logradouro, complemento = @complemento, bairro = @bairro, cep = @cep, dtalteracao = @dtAlteracao WHERE codigo = @codigo;";

                        NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                        command.Parameters.AddWithValue("@codigo", empresa.codigo);
                        command.Parameters.AddWithValue("@razaoSocial", empresa.razaoSocial);
                        command.Parameters.AddWithValue("@nomeFantasia", empresa.nomeFantasia ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@cnpj", empresa.cnpj);
                        command.Parameters.AddWithValue("@ie", empresa.ie ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@telefone", empresa.telefone);
                        command.Parameters.AddWithValue("@email", empresa.email);
                        command.Parameters.AddWithValue("@dtFundacao", empresa.dtFundacao);
                        command.Parameters.AddWithValue("@qtdeCotas", empresa.qtdeCotas);
                        command.Parameters.AddWithValue("@codigoCidade", empresa.codigoCidade);
                        command.Parameters.AddWithValue("@logradouro", empresa.logradouro);
                        command.Parameters.AddWithValue("@complemento", empresa.complemento ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@bairro", empresa.bairro);
                        command.Parameters.AddWithValue("@cep", empresa.cep);
                        command.Parameters.AddWithValue("@dtAlteracao", empresa.dtAlteracao);

                        await command.ExecuteNonQueryAsync();

                        int qtdContas = empresa.contasBancarias.Count;
                        if (qtdContas > 0)
                        {
                            for (int i = 0; i < qtdContas; i++)
                            {
                                ContasBancarias contaBancaria = empresa.contasBancarias[i];
                                contaBancaria.PrepareSave();
                                if (contaBancaria.codigo == 0)
                                {
                                    empresa.contasBancarias[i] = await InsertContaBancaria(conexao, contaBancaria, empresa.codigo);
                                }
                                else if (contaBancaria.codigo > 0 && contaBancaria.status == "Ativo")
                                {
                                    empresa.contasBancarias[i] = await UpdateContaBancaria(conexao, contaBancaria);
                                }
                                else
                                {
                                    await DeleteContaBancaria(conexao, contaBancaria.codigo);
                                    empresa.contasBancarias.RemoveAt(i);
                                }
                            }
                        }

                        transaction.Commit();
                        return empresa;
                    }
                    else
                    {
                        throw new Exception("Empresa já cadastrada");
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

        public override async Task<bool> Excluir(Empresas empresa)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"DELETE FROM contasbancarias WHERE codigoEmpresa = @codigo;
                                   DELETE FROM empresas WHERE codigo = @codigo;";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", empresa.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0 ? true : false;
                }
                catch
                {
                    string sql = @"UPDATE empresas SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo;";

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
