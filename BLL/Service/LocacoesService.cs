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
    public class LocacoesService : IService<Locacoes>
    {
        private readonly LocacoesDAO locacoesDao = null;

        public LocacoesService() => this.locacoesDao = new LocacoesDAO();

        public async Task<string> VerificaDisponibilidadeArea(LocacoesDTO locacao)
        {
            return await locacoesDao.VerificaDisponibilidadeArea(locacao.dtLocacao, locacao.areasLocacao);
        }

        public async Task<IList<Locacoes>> ListarTodos()
        {
            return await locacoesDao.ListarTodos();
        }

        public async Task<Locacoes> BuscarPorID(int codigo)
        {
            return await locacoesDao.BuscarPorID(codigo);
        }

        public async Task<Locacoes> Inserir(Locacoes locacao)
        {
            string error = locacao.Validation();
            if (error == null) {
                locacao.PrepareSave();
                locacao.Ativar();
                return await locacoesDao.Inserir(locacao);
            } else
            {
                throw new Exception(error);
            }
        }

        public async Task<Locacoes> Editar(Locacoes locacao)
        {
            string error = locacao.Validation();
            if (error == null)
            {
                locacao.PrepareSave();
                return await locacoesDao.Editar(locacao);
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
            Locacoes locacao = new Locacoes();
            locacao.codigo = codigo;
            locacao.PrepareSave();
            locacao.Cancelar();
            return await locacoesDao.Excluir(locacao);
        }

        public async Task<IList<Locacoes>> Pesquisar(string str)
        {
            return await locacoesDao.Pesquisar(str);
        }
    }
}
