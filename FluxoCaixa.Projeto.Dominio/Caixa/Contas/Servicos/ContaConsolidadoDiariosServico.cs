using FluxoCaixa.Projeto.Biblioteca.Excecoes;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Repositorios;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Servicos.Interfaces;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Entidades;
using System;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Contas.Servicos
{
    public class ContaConsolidadoDiariosServico : IContaConsolidadoDiariosServico
    {
        private readonly IContaConsolidadoDiarioRepositorio consolidadoRepositorio;

        public ContaConsolidadoDiariosServico(
            IContaConsolidadoDiarioRepositorio consolidadoRepositorio
        )
        {
            this.consolidadoRepositorio = consolidadoRepositorio;
        }

        public async Task<ContaConsolidadoDiario> Validar(int id, string dia)
        {
            DateTime data = DateTime.Parse(dia);

            var consolidado = await consolidadoRepositorio.Recuperar(p => p.Id == id && p.DataDia == data);

            if (consolidado == null)
            {
                throw new RegraDeNegocioExcecao("Consolidado não encontrado.");
            }

            return consolidado;
        }
        
        public async Task<ContaConsolidadoDiario> Validar(DateTime data)
        {
            var consolidado = await consolidadoRepositorio.Recuperar(p => p.DataDia == data);

            if (consolidado == null)
            {
                throw new RegraDeNegocioExcecao("Consolidado não encontrado.");
            }

            return consolidado;
        }

        public async Task<ContaConsolidadoDiario> RecuperarOuInserir(Conta conta)
        {
            var hoje = DateTime.Now.Date;

            var consolidado = await consolidadoRepositorio.Recuperar(p => p.DataDia == hoje);

            if (consolidado == null)
            {
                consolidado = new ContaConsolidadoDiario(conta);

                await consolidadoRepositorio.Inserir(consolidado);
            }

            return consolidado;
        }

        public async Task<ContaConsolidadoDiario> AtualizarTotalCredito(Lancamento lancamento)
        {
            Conta conta = lancamento.Conta;

            var consolidadoDiario = await RecuperarOuInserir(conta);

            consolidadoDiario.VerificaSeODiaEstaConsolidado();

            consolidadoDiario.IncrementarTotalCredito(lancamento.Valor);
            consolidadoDiario.AtualizarSaldo();

            await consolidadoRepositorio.Editar(consolidadoDiario);

            return consolidadoDiario;
        }
        
        public async Task<ContaConsolidadoDiario> AtualizarTotalDebito(Lancamento lancamento)
        {
            Conta conta = lancamento.Conta;

            var consolidadoDiario = await RecuperarOuInserir(conta);

            consolidadoDiario.VerificaSeODiaEstaConsolidado();

            consolidadoDiario.IncrementarTotalDebito(lancamento.Valor);
            consolidadoDiario.AtualizarSaldo();

            await consolidadoRepositorio.Editar(consolidadoDiario);

            return consolidadoDiario;
        }

        public async Task<ContaConsolidadoDiario> Consolidar(int id, string dia)
        {
            var consolidadoDiario = await Validar(id, dia);
            var hoje = DateTime.Now;

            consolidadoDiario.Consolidar();
            consolidadoDiario.SetDataConsolidacao(hoje);

            await consolidadoRepositorio.Editar(consolidadoDiario);

            return consolidadoDiario;
        }
    }
}
