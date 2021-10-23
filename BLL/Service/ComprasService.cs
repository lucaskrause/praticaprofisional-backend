using BLL.DataTransferObjects;
using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace BLL.Service
{
    public class ComprasService : IService<Compras>
    {
        private readonly ComprasDAO comprasDao = null;

        public ComprasService() => this.comprasDao = new ComprasDAO();

        public async Task<bool> Find(Compras compra)
        {
            return await comprasDao.Find(compra);
        }

        public async Task<IList<Compras>> ListarTodos()
        {
            return await comprasDao.ListarTodos();
        }

        public async Task<Compras> BuscarPorID(int id)
        {
            return await comprasDao.BuscarPorID(id);
        }

        public async Task<Compras> BuscarCompra(Compras compra)
        {
            return await comprasDao.BuscarCompra(compra);
        }

        public async Task<Compras> Inserir(Compras compra)
        {
            compra.PrepareSave();
            compra.Ativar();
            return await comprasDao.Inserir(compra);
        }

        public async Task<Compras> Editar(Compras compra)
        {
            compra.PrepareSave();
            return await comprasDao.Editar(compra);
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
                    dtVencimento = parcela.dtEmissao.AddDays((double) p.numeroDias),
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
            Compras compra = new Compras();
            compra.PrepareSave();
            compra.Cancelar();
            return await comprasDao.Excluir(compra);
        }

        public async Task<bool> Cancelar(Compras compra)
        {
            compra.PrepareSave();
            compra.Cancelar();
            return await comprasDao.Excluir(compra);
        }

        public async Task<IList<Compras>> Pesquisar(string str)
        {
            return await comprasDao.Pesquisar(str);
        }
    }
}
