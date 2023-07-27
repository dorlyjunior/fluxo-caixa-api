using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Requests;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Responses;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Aplicacao.Caixa.Contas.Servicos.Interfaces
{
    public interface IContasAppServico
    {
        Task<PaginacaoResponse<ContaResponse>> Recuperar(ContaListagemRequest request);
        Task<ContaResponse> Recuperar(int id);
        Task<ContaResponse> Inserir(ContaCadastroEdicaoRequest request);
        Task<ContaResponse> Editar(int id, ContaCadastroEdicaoRequest request);
        Task<ContaResponse> Inativar(int id);
    }
}
