using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CotasService : IService<Cotas>
    {
        private readonly CotasDAO cotasDao = null;

        public CotasService() => this.cotasDao = new CotasDAO();

        public string validaCota(Cotas cota)
        {
            if (cota.codigoCliente <= 0)
            {
                return "Cliente obrigatório";
            }
            else if (cota.valor <= 0)
            {
                return "Valor obrigatória";
            }
            else if (cota.dtInicio == null || cota.dtInicio.Date < (DateTime.Now).Date)
            {
                return "Data de Inicio obrigatória, e deve ser data de hoje ou maior";
            }
            else if (cota.dtTermino == null || cota.dtTermino <= cota.dtInicio)
            {
                return "Data de Termino obrigatória, e deve ser maior que a data de inicio";
            }
            else
            {
                return null;
            }
        }

        public async Task<IList<Cotas>> ListarTodos()
        {
            return await cotasDao.ListarTodos();
        }
     
        public async Task<Cotas> BuscarPorID(int codigo)
        {
            return await cotasDao.BuscarPorID(codigo);
        }

        public async Task<Cotas> Inserir(Cotas cota)
        {
            string error = validaCota(cota);
            if (error == null) {
                cota.codigoEmpresa = 1;
                cota.Ativar();
                cota.PrepareSave();
                return await cotasDao.Inserir(cota);
            } else
            {
                throw new Exception(error);
            }
        }

        public async Task<Cotas> Editar(Cotas cota)
        {
            string error = validaCota(cota);
            if (error == null)
            {
                cota.codigoEmpresa = 1;
                cota.PrepareSave();
                return await cotasDao.Editar(cota);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            Cotas cota = new Cotas();
            cota.codigo = codigo;
            cota.Inativar();
            cota.PrepareSave();
            return await cotasDao.Excluir(cota);
        }

        public async Task<IList<Cotas>> Pesquisar(string str)
        {
            return await cotasDao.Pesquisar(str);
        }
    }
}
