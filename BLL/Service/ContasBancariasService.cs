using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ContasBancariasService : IService<ContasBancarias>
    {
        private readonly ContasBancariasDAO contasBancariasDao = null;

        public ContasBancariasService() => this.contasBancariasDao = new ContasBancariasDAO();

        public async Task<IList<ContasBancarias>> BuscarPorEmpresa(int codigo)
        {
            return await contasBancariasDao.BuscarPorEmpresa(codigo);
        }

        public async Task<IList<ContasBancarias>> ListarTodos()
        {
            return await contasBancariasDao.ListarTodos();
        }

        public async Task<ContasBancarias> BuscarPorID(int codigo)
        {
            return await contasBancariasDao.BuscarPorID(codigo);
        }

        public async Task<ContasBancarias> Inserir(ContasBancarias contaBancaria)
        {
            string error = contaBancaria.Validation();
            if (error == null) {
                contaBancaria.Ativar();
                contaBancaria.PrepareSave();
                return await contasBancariasDao.Inserir(contaBancaria);
            } else
            {
                throw new Exception(error);
            }
        }

        public async Task<ContasBancarias> Editar(ContasBancarias contaBancaria)
        {
            string error = contaBancaria.Validation();
            if (error == null)
            {
                contaBancaria.PrepareSave();
                return await contasBancariasDao.Editar(contaBancaria);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            ContasBancarias contaBancaria = new ContasBancarias();
            contaBancaria.codigo = codigo;
            contaBancaria.Inativar();
            contaBancaria.PrepareSave();
            return await contasBancariasDao.Excluir(contaBancaria);
        }

        public async Task<IList<ContasBancarias>> Pesquisar(string str)
        {
            return await contasBancariasDao.Pesquisar(str);
        }
    }
}
