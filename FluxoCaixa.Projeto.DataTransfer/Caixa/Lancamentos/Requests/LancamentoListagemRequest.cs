using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using System;

namespace FluxoCaixa.Projeto.DataTransfer.Caixa.Lancamentos.Requests
{
    public class LancamentoListagemRequest : PaginacaoRequest
    {
        public int Id { get; set; }
        public int IdConta { get; set; }
        public string IdTransacao { get; set; }
        public string Descricao { get; set; }
        public decimal ValorDe { get; set; }
        public decimal ValorAte { get; set; }
        public DateTime DataLancamentoDe { get; set; }
        public DateTime DataLancamentoAte { get; set; }
        public int CodigoStatus { get; set; }
        public int CodigoTipo { get; set; }
    }
}
