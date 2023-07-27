using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Enumeradores;
using System;

namespace FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Responses
{
    public class ContaConsolidadoDiarioResponse
    {
        public int Id { get; set; }
        public string DiaSemana { get; set; }
        public DateTime DataDia { get; set; }
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public decimal TotalCredito { get; set; }
        public decimal TotalDebito { get; set; }
        public decimal Saldo { get; set; }
        public string Status { get; set; }
        public int CodigoStatus { get; set; }
        public DateTime DataConsolidacao { get; set; }
        public ContaResponse Conta { set; get; }
    }
}
