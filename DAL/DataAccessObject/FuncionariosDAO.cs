﻿using DAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccessObject
{
    public class FuncionariosDAO : DAO<Funcionarios>
    {
        public override async Task<IList<Funcionarios>> ListarTodos()
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    List<Funcionarios> list = await GetResultSet(command);
                    return list;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Funcionarios> BuscarPorID(int codigo)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@codigo", codigo);

                    List<Funcionarios> list = await GetResultSet(command);
                    return list[0];
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Funcionarios> Inserir(Funcionarios funcionario)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@nome", funcionario.nome);
                    command.Parameters.AddWithValue("@cpf", funcionario.cpf);
                    command.Parameters.AddWithValue("@rg", funcionario.rg);
                    command.Parameters.AddWithValue("@sexo", funcionario.sexo);
                    command.Parameters.AddWithValue("@email", funcionario.email);
                    command.Parameters.AddWithValue("@telefone", funcionario.telefone);
                    command.Parameters.AddWithValue("@dtnascfundacao", funcionario.dtNascimento);
                    command.Parameters.AddWithValue("@codigoCidade", funcionario.codigoCidade);
                    command.Parameters.AddWithValue("@logradouro", funcionario.logradouro);
                    command.Parameters.AddWithValue("@complemento", funcionario.complemento);
                    command.Parameters.AddWithValue("@bairro", funcionario.bairro);
                    command.Parameters.AddWithValue("@cep", funcionario.cep);
                    command.Parameters.AddWithValue("@codigoEmpresa", funcionario.codigoEmpresa);
                    command.Parameters.AddWithValue("@salario", funcionario.salario);
                    command.Parameters.AddWithValue("@dtAdmissao", funcionario.dtAdmissao);
                    command.Parameters.AddWithValue("@dtDemissao", funcionario.dtDemissao);
                    command.Parameters.AddWithValue("@dtCadastro", funcionario.dtCadastro);
                    command.Parameters.AddWithValue("@dtAlteracao", funcionario.dtAlteracao);
                    command.Parameters.AddWithValue("@status", funcionario.status);

                    Object idInserido = await command.ExecuteScalarAsync();
                    funcionario.codigo = (int)idInserido;
                    return funcionario;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<Funcionarios> Editar(Funcionarios funcionario)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@nome", funcionario.nome);
                    command.Parameters.AddWithValue("@cpf", funcionario.cpf);
                    command.Parameters.AddWithValue("@rg", funcionario.rg);
                    command.Parameters.AddWithValue("@sexo", funcionario.sexo);
                    command.Parameters.AddWithValue("@email", funcionario.email);
                    command.Parameters.AddWithValue("@telefone", funcionario.telefone);
                    command.Parameters.AddWithValue("@dtnascfundacao", funcionario.dtNascimento);
                    command.Parameters.AddWithValue("@codigoCidade", funcionario.codigoCidade);
                    command.Parameters.AddWithValue("@logradouro", funcionario.logradouro);
                    command.Parameters.AddWithValue("@complemento", funcionario.complemento);
                    command.Parameters.AddWithValue("@bairro", funcionario.bairro);
                    command.Parameters.AddWithValue("@cep", funcionario.cep);
                    command.Parameters.AddWithValue("@codigoEmpresa", funcionario.codigoEmpresa);
                    command.Parameters.AddWithValue("@salario", funcionario.salario);
                    command.Parameters.AddWithValue("@dtAdmissao", funcionario.dtAdmissao);
                    command.Parameters.AddWithValue("@dtDemissao", funcionario.dtDemissao);
                    command.Parameters.AddWithValue("@dtAlteracao", funcionario.dtAlteracao);

                    await command.ExecuteNonQueryAsync();
                    return funcionario;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<bool> Excluir(Funcionarios funcionario)
        {
            using (var conexao = GetCurrentConnection())
            {
                try
                {
                    string sql = @"UPDATE funcionarios SET status = @status, dtAlteracao = @dtAlteracao WHERE codigo = @codigo";
                    // string sql = @"DELETE FROM funcionarios WHERE codigo = @codigo";

                    conexao.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sql, conexao);

                    command.Parameters.AddWithValue("@status", funcionario.status);
                    command.Parameters.AddWithValue("@dtAlteracao", funcionario.dtAlteracao);
                    command.Parameters.AddWithValue("@codigo", funcionario.codigo);

                    var result = await command.ExecuteNonQueryAsync();
                    return result == 1 ? true : false;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        public override async Task<IList<Funcionarios>> Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}