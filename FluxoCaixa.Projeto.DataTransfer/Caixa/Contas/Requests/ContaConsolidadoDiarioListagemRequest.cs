using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using System;

namespace FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Requests
{
    public class ContaConsolidadoDiarioListagemRequest : PaginacaoRequest
    {
        public int Id { get; set; }
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string DiaSemana { get; set; }
        public DateTime DataDia { get; set; }
        public DateTime DataConsolidacaoDe { get; set; }
        public DateTime DataConsolidacaoAte { get; set; }
        public int CodigoStatus { get; set; }
    }
}
