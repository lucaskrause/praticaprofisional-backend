using BLL.DataTransferObjects;
using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CotasService : IService<Cotas>
    {
        private readonly CotasDAO cotasDao = null;

        public CotasService() => this.cotasDao = new CotasDAO();

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
            string error = cota.Validation();
            if (error == null) {
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
            string error = cota.Validation();
            if (error == null)
            {
                cota.PrepareSave();
                return await cotasDao.Editar(cota);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<List<ParcelasCompra>> gerarParcelas(ParcelasDTO parcela)
        {
            CondicoesPagamentoDAO condicaoDao = new CondicoesPagamentoDAO();

            CondicoesPagamento condicao = await condicaoDao.BuscarPorID(parcela.codigoCondicaoPagamento);

            List<ParcelasCompra> listParcelas = new List<ParcelasCompra>();

            foreach (var p in condicao.parcelas)
            {
                var itemParcela = new ParcelasCompra
                {
                    numeroParcela = p.numeroParcela,
                    dtEmissao = DateTime.Now,
                    dtVencimento = parcela.dtEmissao.AddDays((double)p.numeroDias),
                    codigoFormaPagamento = p.codigoFormaPagamento,
                    descricaoForma = p.formaPagamento.descricao,
                    valorParcela = decimal.Round(((p.porcentagem / 100) * parcela.valorTotal), 2)
                };
                listParcelas.Add(itemParcela);
            }

            var totalParcelas = listParcelas.Sum(k => k.valorParcela);
            if (totalParcelas != parcela.valorTotal)
            {
                if (totalParcelas < parcela.valorTotal)
                {
                    var dif = parcela.valorTotal - totalParcelas;
                    var list = listParcelas.OrderBy(u => u.numeroParcela);
                    list.Last().valorParcela = list.Last().valorParcela + dif;
                    listParcelas = list.ToList();
                }
                if (totalParcelas > parcela.valorTotal)
                {
                    var dif = totalParcelas - parcela.valorTotal;
                    var list = listParcelas.OrderBy(u => u.numeroParcela);
                    list.Last().valorParcela = list.Last().valorParcela - dif;
                    listParcelas = list.ToList();
                }
            }

            return listParcelas;
        }

        public async Task<bool> Excluir(int codigo)
        {
            Cotas cota = new Cotas();
            cota.codigo = codigo;
            cota.Cancelar();
            cota.PrepareSave();
            return await cotasDao.Excluir(cota);
        }

        public async Task<IList<Cotas>> Pesquisar(string str)
        {
            return await cotasDao.Pesquisar(str);
        }
    }
}
