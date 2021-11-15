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
    public class OrdensServicoService : IService<OrdensServico>
    {
        private readonly OrdensServicoDAO ordensDao = null;

        public OrdensServicoService() => this.ordensDao = new OrdensServicoDAO();

        public async Task<IList<OrdensServico>> ListarTodos()
        {
            return await ordensDao.ListarTodos();
        }

        public async Task<OrdensServico> BuscarPorID(int codigo)
        {
            return await ordensDao.BuscarPorID(codigo);
        }

        public async Task<OrdensServico> Inserir(OrdensServico ordemServico)
        {
            ordemServico.PrepareSave();
            ordemServico.Ativar();
            return await ordensDao.Inserir(ordemServico);
        }

        public async Task<OrdensServico> Editar(OrdensServico ordemServico)
        {
            ordemServico.PrepareSave();
            return await ordensDao.Editar(ordemServico);
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
            OrdensServico compra = new OrdensServico();
            compra.PrepareSave();
            compra.Cancelar();
            return await ordensDao.Excluir(compra);
        }

        public async Task<bool> Cancelar(OrdensServico compra)
        {
            compra.PrepareSave();
            compra.Cancelar();
            return await ordensDao.Excluir(compra);
        }

        public async Task<IList<OrdensServico>> Pesquisar(string str)
        {
            return await ordensDao.Pesquisar(str);
        }
    }
}
