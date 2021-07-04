using DAL.DataAccessObject;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CondicoesPagamentoService : IService<CondicoesPagamento>
    {
        private readonly CondicoesPagamentoDAO condicoesPagamentoDao = null;

        public CondicoesPagamentoService() => this.condicoesPagamentoDao = new CondicoesPagamentoDAO();

        public string validaParcelas(List<CondicoesParcelas> parcelas)
        {
            if (parcelas.Count > 0)
            {
                decimal perc = 100;
                for (int i = 0; i < parcelas.Count; i++)
                {
                    CondicoesParcelas parcela = parcelas[i];
                    perc -= parcela.porcentagem;

                    if (parcela.numeroDias < 1 && parcela.numeroDias <= parcelas[i - 1].numeroDias)
                    {
                        return "Número de dias precisa ser maior que 0 e maior que a parcela anterior";
                    }
                    else if (parcela.porcentagem <= 0)
                    {
                        return "Porcentagem é obrigatória em todas as parcelas";
                    }
                    else if (parcela.codigoFormaPagamento <= 0)
                    {
                        return "Forma de Pagamento é obrigatória em todas as parcelas";
                    }
                    else
                    {
                        continue;
                    }
                }
                
                if (perc != 0)
                {
                    return "A soma das porcentagens está inválida";
                }
                else
                {
                    return null;
                }
            } else
            {
                return "Adicione pelo menos 1 parcela";
            }
        }

        public string validaCondicaoPagamento(CondicoesPagamento condicaoPagamento)
        {
            if (condicaoPagamento.descricao == null || condicaoPagamento.descricao == "")
            {
                return "Condição de Pagamento obrigatório";
            }
            else if (condicaoPagamento.multa < 0)
            {
                return "Multa obrigatória";
            }
            else if (condicaoPagamento.juros < 0)
            {
                return "Juros obrigatório";
            }
            else if (condicaoPagamento.desconto < 0)
            {
                return "Descontro obrigatório";
            }
            else
            {
                string error = validaParcelas(condicaoPagamento.parcelas);
                if (error != null)
                {
                    return error;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<IList<CondicoesPagamento>> ListarTodos()
        {
            return await condicoesPagamentoDao.ListarTodos();
        }

        public async Task<CondicoesPagamento> BuscarPorID(int codigo)
        {
            return await condicoesPagamentoDao.BuscarPorID(codigo);
        }

        public async Task<CondicoesPagamento> Inserir(CondicoesPagamento condicaoPagamento)
        {
            string error = validaCondicaoPagamento(condicaoPagamento);
            if (error == null)
            {
                condicaoPagamento.Ativar();
                condicaoPagamento.PrepareSave();
                return await condicoesPagamentoDao.Inserir(condicaoPagamento);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<CondicoesPagamento> Editar(CondicoesPagamento condicaoPagamento)
        {
            string error = validaCondicaoPagamento(condicaoPagamento);
            if (error == null)
            {
                condicaoPagamento.PrepareSave();
                return await condicoesPagamentoDao.Editar(condicaoPagamento);
            }
            else
            {
                throw new Exception(error);
            }
        }

        public async Task<bool> Excluir(int codigo)
        {
            CondicoesPagamento condicoesPagamento = new CondicoesPagamento();
            condicoesPagamento.codigo = codigo;
            condicoesPagamento.Inativar();
            condicoesPagamento.PrepareSave();
            return await condicoesPagamentoDao.Excluir(condicoesPagamento);
        }

        public async Task<IList<CondicoesPagamento>> Pesquisar(string str)
        {
            return await condicoesPagamentoDao.Pesquisar(str);
        }
    }
}
