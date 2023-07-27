using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using System.Threading.Tasks;
using System;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Entidades;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Contas.Servicos.Interfaces
{
    public interface IContaConsolidadoDiariosServico
    {
        Task<ContaConsolidadoDiario> Validar(int id, string dia);
        Task<ContaConsolidadoDiario> Validar(DateTime data);
        Task<ContaConsolidadoDiario> RecuperarOuInserir(Conta conta);
        Task<ContaConsolidadoDiario> AtualizarTotalCredito(Lancamento lancamento);
        Task<ContaConsolidadoDiario> AtualizarTotalDebito(Lancamento lancamento);
        Task<ContaConsolidadoDiario> Consolidar(int id, string dia);
    }
}
