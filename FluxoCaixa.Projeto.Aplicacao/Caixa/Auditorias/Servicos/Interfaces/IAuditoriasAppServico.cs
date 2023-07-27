using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Auditorias.Requests;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Auditorias.Responses;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Aplicacao.Caixa.Auditorias.Servicos.Interfaces
{
    public interface IAuditoriasAppServico
    {
        Task<PaginacaoResponse<AuditoriaResponse>> Recuperar(AuditoriaListagemRequest request);
    }
}
