using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Lancamentos.Requests;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Lancamentos.Responses;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Aplicacao.Caixa.Lancamentos.Servicos.Interfaces
{
    public interface ILancamentosAppServico
    {
        Task<PaginacaoResponse<LancamentoResponse>> Recuperar(LancamentoListagemRequest request);
        Task<LancamentoResponse> Recuperar(int id);
        Task<LancamentoResponse> LancarCredito(LancamentoCadastroRequest request);
        Task<LancamentoResponse> LancarDebito(LancamentoCadastroRequest request);
        Task<LancamentoResponse> RemoverCredito(int id, string idTransacao);
        Task<LancamentoResponse> RemoverDebito(int id, string idTransacao);
    }
}
