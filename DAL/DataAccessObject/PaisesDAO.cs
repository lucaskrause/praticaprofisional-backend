using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL.DataAccessObject
{
    public class PaisesDAO : DAO<Paises>
    {
        public PaisesDAO() : base()
        {
        }

        public override Paises BuscarPorID(Paises entity)
        {
            throw new NotImplementedException();
        }

        public override Paises Editar(Paises obj)
        {
            throw new NotImplementedException();
        }

        public override bool Excluir(Paises entity)
        {
            throw new NotImplementedException();
        }

        public override Paises Inserir(Paises pais)
        {
            using (var conexao = GetCurrentConnection())
            {
                string sql = @"INSERT INTO paises(pais, sigla, dtCadastro, dtAlteracao, status) values (@pais, @sigla, @dtCadastro, @dtAlteracao, @status)";

                SqlCommand command = new SqlCommand(sql, conexao);

                command.Parameters.AddWithValue("@pais", pais.Pais);
                command.Parameters.AddWithValue("@sigla", pais.Sigla);
                command.Parameters.AddWithValue("@dtCadastro", pais.dtCadastro);
                command.Parameters.AddWithValue("@dtAlteracao", pais.dtAlteracao);
                command.Parameters.AddWithValue("@status", pais.Status);

                conexao.Open();
                command.ExecuteNonQuery();
                conexao.Close();

                return pais;
            }
        }

        public override IList<Paises> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public override Paises Pesquisar(string str)
        {
            throw new NotImplementedException();
        }
    }
}
