using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class CondicoesPagamento : Pai
    {
        public int totalParcelas { get; set; }

        public string descricao { get; set; }

        public decimal multa { get; set; }

        public decimal juros { get; set; }

        public decimal desconto { get; set; }

        public List<CondicoesParcelas> parcelas { get; set; }

        public override string Validation()
        {
            if (this.descricao == null || this.descricao == "")
            {
                return "Condição de Pagamento obrigatório";
            }
            else if (this.multa < 0)
            {
                return "Multa obrigatória";
            }
            else if (this.juros < 0)
            {
                return "Juros obrigatório";
            }
            else if (this.desconto < 0)
            {
                return "Descontro obrigatório";
            }
            else
            {
                if (parcelas.Count > 0)
                {
                    decimal perc = 100;
                    for (int i = 0; i < parcelas.Count; i++)
                    {
                        CondicoesParcelas parcela = parcelas[i];
                        perc -= parcela.porcentagem;
                        int numDiasAnt = i == 0 ? 0 : parcelas[i - 1].numeroDias;
                        string error = parcela.Validation();

                        if(error == null)
                        {
                            if (parcela.numeroDias <= numDiasAnt)
                            {
                                return "Número de dias das parcelas devem ser crescente e não pode se repetir";
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            return error;
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
                }
                else
                {
                    return "Adicione pelo menos 1 parcela";
                }
            }
        }
    }
}
