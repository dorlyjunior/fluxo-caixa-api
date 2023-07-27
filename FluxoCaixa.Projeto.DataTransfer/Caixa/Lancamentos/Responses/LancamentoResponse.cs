using FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Responses;
using System;

namespace FluxoCaixa.Projeto.DataTransfer.Caixa.Lancamentos.Responses
{
    public class LancamentoResponse
    {
        public int Id { get; set; }
        public string IdTransacao { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataLancamento { get; set; }
        public string Tipo { get; set; }
        public int CodigoTipo { get; set; }
        public string Status { get; set; }
        public int CodigoStatus { get; set; }
        public ContaResponse Conta { get; set; }
    }
}
