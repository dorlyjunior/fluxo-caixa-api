using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Requests;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Responses;
using System;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Aplicacao.Caixa.Contas.Servicos.Interfaces
{
    public interface IContaConsolidadoDiariosAppServico
    {
        Task<PaginacaoResponse<ContaConsolidadoDiarioResponse>> Recuperar(ContaConsolidadoDiarioListagemRequest request);
        Task<ContaConsolidadoDiarioResponse> Recuperar(int id, string dia);
        Task<ContaConsolidadoDiarioResponse> ConsolidarDia(int id, string dia);
    }
}
