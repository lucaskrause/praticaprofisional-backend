using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class FuncionariosService : IService<Funcionarios>
    {
        private readonly FuncionariosDAO funcionariosDao = null;

        public FuncionariosService() => this.funcionariosDao = new FuncionariosDAO();

        public async Task<IList<Funcionarios>> ListarTodos()
        {
            return await funcionariosDao.ListarTodos();
        }

        public async Task<Funcionarios> BuscarPorID(int codigo)
        {
            return await funcionariosDao.BuscarPorID(codigo);
        }

        public async Task<Funcionarios> Inserir(Funcionarios funcionario)
        {
            string error = funcionario.Validation();
            if (error == null) {
                funcionario.codigoEmpresa = 1;
                funcionario.Ativar();
                funcionario.PrepareSave();
                return await funcionariosDao.Inserir(funcionario);
            } else
            {
                throw new Exception(error);
            }
        }

        public async Task<Funcionarios> Editar(Funcionarios funcionario)
        {
            string error = funcionario.Validation();
            if (error == null)
            {
                funcionario.codigoEmpresa = 1;
                funcionario.PrepareSave();
                return await funcionariosDao.Editar(funcionario);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            Funcionarios funcionario = new Funcionarios();
            funcionario.codigo = codigo;
            funcionario.Inativar();
            funcionario.PrepareSave();
            return await funcionariosDao.Excluir(funcionario);
        }

        public async Task<IList<Funcionarios>> Pesquisar(string str)
        {
            return await funcionariosDao.Pesquisar(str);
        }
    }
}
