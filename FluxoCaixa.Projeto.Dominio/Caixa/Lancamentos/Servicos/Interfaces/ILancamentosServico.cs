using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Entidades;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Servicos.Interfaces
{
    public interface ILancamentosServico
    {
        Task<Lancamento> Validar(int id);
        Task<Lancamento> Validar(int id, string idTransacao);
        Task<Lancamento> InserirCredito(int idConta, string descricao, decimal valor);
        Task<Lancamento> InserirDebito(int idConta, string descricao, decimal valor);
        Task<Lancamento> CancelarLancamentoCredito(int id, string idTransacao);
        Task<Lancamento> CancelarLancamentoDebito(int id, string idTransacao);
    }
}
