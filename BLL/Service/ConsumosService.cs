using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ConsumosService : IService<Consumos>
    {
        private readonly ConsumosDAO consumosDao = null;

        public ConsumosService() => this.consumosDao = new ConsumosDAO();

        public async Task<IList<Consumos>> ListarTodos()
        {
            return await consumosDao.ListarTodos();
        }

        public async Task<Consumos> BuscarPorID(int id)
        {
            return await consumosDao.BuscarPorID(id);
        }

        public async Task<Consumos> Inserir(Consumos consumo)
        {
            string error = consumo.Validation();
            if (error == null)
            {
                consumo.PrepareSave();
                consumo.Ativar();
                return await consumosDao.Inserir(consumo);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<Consumos> Editar(Consumos consumo)
        {
            string error = consumo.Validation();
            if (error == null)
            {
                consumo.PrepareSave();
                return await consumosDao.Editar(consumo);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            Consumos consumo = new Consumos();
            consumo.codigo = codigo;
            consumo.PrepareSave();
            consumo.Inativar();
            return await consumosDao.Excluir(consumo);
        }

        public async Task<IList<Consumos>> Pesquisar(string str)
        {
            return await consumosDao.Pesquisar(str);
        }
    }
}
