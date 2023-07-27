using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Contas.Servicos.Interfaces
{
    public interface IContasServico
    {
        Task<Conta> Validar(int id);
        Task<Conta> Inserir(string nome);
        Task<Conta> Atualizar(int id, string nome);
        Task<Conta> IncrementarSaldo(Conta conta, decimal saldo);
        Task<Conta> DecrementarSaldo(Conta conta, decimal saldo);
        Task<Conta> Inativar(int id);
    }
}
