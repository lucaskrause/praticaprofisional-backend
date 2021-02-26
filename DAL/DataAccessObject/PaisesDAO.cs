using RUPsystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL.DataAccessObject
{
    public class PaisesDAO : DAO
    {
        public PaisesDAO() : base()
        {
        }

        public override void Inserir(object obj)
        {
            using (var conexao = GetCurrentConnection())
            {
                Paises pais = obj as Paises;
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
            }
        }
    }
}
