using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Reservas : AbstractEntity
    {
        public int codigoEmpresa { get; set; }

        public Empresas empresa { get; set; }

        public int codigoCliente { get; set; }

        public Clientes cliente { get; set; }

        public string nomeCliente { set { cliente ??= new Clientes(); cliente.nome = value; } }

        public int qtdePessoas { get; set; }

        public DateTime dtReserva { get; set; }

        public decimal valor { get; set; }

        public int codigoCondicaoPagamento { get; set; }

        public CondicoesPagamento condicaoPagamento { get; set; }

        public string nomeCondicao { set { condicaoPagamento ??= new CondicoesPagamento(); condicaoPagamento.descricao = value; } }

        public List<AreasLocacao> areasLocacao { get; set; }

        public override string Validation()
        {
            if (this.codigoCliente <= 0)
            {
                return "Cliente obrigatório";
            }
            else if (this.qtdePessoas <= 0)
            {
                return "Quantidade de Pessoas obrigatória";
            }
            else if (this.dtReserva == null || this.dtReserva.Date < (DateTime.Now).Date)
            {
                return "Data da Reserva obrigatória";
            }
            else if (this.valor <= 0)
            {
                return "Data da Reserva obrigatória";
            }
            else if (this.codigoCondicaoPagamento <= 0)
            {
                return "Condição de Pagamento obrigatório";
            }
            else if (this.areasLocacao == null || this.areasLocacao.Count == 0)
            {
                return "Áreas de Locação obrigatório";
            }
            else
            {
                return null;
            }
        }
    }
}
