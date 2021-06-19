using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CategoriasService : IService<Categorias>
    {
        private readonly CategoriasDAO categoriasDao = null;

        public CategoriasService() => this.categoriasDao = new CategoriasDAO();

        public async Task<IList<Categorias>> ListarTodos()
        {
            return await categoriasDao.ListarTodos();
        }
        
        public async Task<Categorias> BuscarPorID(int id)
        {
            return await categoriasDao.BuscarPorID(id);
        }

        public async Task<Categorias> Inserir(Categorias categoria)
        {
            categoria.PrepareSave();
            categoria.Ativar();
            return await categoriasDao.Inserir(categoria);
        }

        public async Task<Categorias> Editar(Categorias categoria)
        {
            categoria.PrepareSave();
            return await categoriasDao.Editar(categoria);
        }

        public async Task<bool> Excluir(int codigo)
        {
            Categorias categoria = new Categorias();
            categoria.codigo = codigo;
            categoria.PrepareSave();
            categoria.Inativar();
            return await categoriasDao.Excluir(categoria);
        }

        public async Task<IList<Categorias>> Pesquisar(string str)
        {
            return await categoriasDao.Pesquisar(str);
        }
    }
}
