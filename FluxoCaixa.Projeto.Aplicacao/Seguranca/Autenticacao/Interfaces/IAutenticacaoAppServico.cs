using FluxoCaixa.Projeto.DataTransfer.Seguranca.Autenticacao.Requests;
using FluxoCaixa.Projeto.DataTransfer.Seguranca.Autenticacao.Responses;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Aplicacao.Seguranca.Autenticacao.Interfaces
{
    public interface IAutenticacaoAppServico
    {
        Task<AutenticacaoResponse> AutenticarUsuario(AutenticacaoRequest request);
    }
}
