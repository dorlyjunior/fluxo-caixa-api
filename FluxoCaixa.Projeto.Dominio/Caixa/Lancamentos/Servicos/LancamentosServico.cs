using FluxoCaixa.Projeto.Biblioteca.Excecoes;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Servicos.Interfaces;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Enumeradores;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Repositorios;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Servicos.Interfaces;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Servicos
{
    public class LancamentosServico : ILancamentosServico
    {
        private readonly ILancamentoRepositorio lancamentoRepositorio;
        private readonly IContasServico contasServico;
        private readonly IContaConsolidadoDiariosServico consolidadosServico;

        public LancamentosServico(
            ILancamentoRepositorio lancamentoRepositorio,
            IContasServico contasServico,
            IContaConsolidadoDiariosServico consolidadosServico
        )
        {
            this.lancamentoRepositorio = lancamentoRepositorio;
            this.contasServico = contasServico;
            this.consolidadosServico = consolidadosServico;
        }

        public async Task<Lancamento> Validar(int id)
        {
            var lancamento = await lancamentoRepositorio.RecuperarDetalhe(id);

            if (lancamento == null)
            {
                throw new RegraDeNegocioExcecao("Lançamento não encontrado.");
            }

            return lancamento;
        }
        
        public async Task<Lancamento> Validar(int id, string idTransacao)
        {
            var lancamento = await lancamentoRepositorio.Recuperar(p => p.Id == id && p.IdTransacao == idTransacao);

            if (lancamento == null)
            {
                throw new RegraDeNegocioExcecao("Lançamento não encontrado.");
            }

            return lancamento;
        }

        public async Task<Lancamento> InserirCredito(int idConta, string descricao, decimal valor)
        {
            var conta = await contasServico.Validar(idConta);
            bool estaInativa = conta.VerificaSeEstaInativa();

            if(estaInativa) { throw new RegraDeNegocioExcecao("Operação inválida! A conta encontra-se desativada e não é possível fazer lançamentos."); }

            var lancamento = new Lancamento(conta, LancamentoTipoEnum.Credito, descricao, valor);

            await lancamentoRepositorio.Inserir(lancamento);

            var consolidadoDiario = await consolidadosServico.AtualizarTotalCredito(lancamento);
            
            await contasServico.IncrementarSaldo(consolidadoDiario.Conta, lancamento.Valor);

            return lancamento;
        }        

        public async Task<Lancamento> InserirDebito(int idConta, string descricao, decimal valor)
        {
            var conta = await contasServico.Validar(idConta);

            bool estaInativa = conta.VerificaSeEstaInativa();

            if (estaInativa) { throw new RegraDeNegocioExcecao("Operação inválida! A conta encontra-se desativada e não é possível fazer lançamentos."); }

            var lancamento = new Lancamento(conta, LancamentoTipoEnum.Debito, descricao, valor);

            await lancamentoRepositorio.Inserir(lancamento);

            // Atualizando totais de débitos e saldo diário
            var consolidadoDiario = await consolidadosServico.AtualizarTotalDebito(lancamento);

            // Atualizando saldo da conta
            await contasServico.DecrementarSaldo(consolidadoDiario.Conta, lancamento.Valor);

            return lancamento;
        }

        public async Task<Lancamento> CancelarLancamentoCredito(int id, string idTransacao)
        {
            var lancamento = await Validar(id, idTransacao);
            
            if (lancamento.Status == LancamentoStatusEnum.Cancelado) { throw new RegraDeNegocioExcecao("Operação inválida! Lançamento já se encontra cancelado."); }

            var descricao = $"Lançamento de débito gerado a partir da remoção do crédito de ID {lancamento.IdTransacao}";

            var lancamentoDebito = await InserirDebito(lancamento.Conta.Id, descricao, lancamento.Valor);

            lancamento.SetStatus(LancamentoStatusEnum.Cancelado);

            await lancamentoRepositorio.Editar(lancamento);

            return lancamentoDebito;
        }
        
        public async Task<Lancamento> CancelarLancamentoDebito(int id, string idTransacao)
        {
            var lancamento = await Validar(id, idTransacao);
            
            if (lancamento.Status == LancamentoStatusEnum.Cancelado) { throw new RegraDeNegocioExcecao("Operação inválida! Lançamento já se encontra cancelado."); }

            var descricao = $"Lançamento de crédito gerado a partir da remoção do débito de ID {lancamento.IdTransacao}";

            var lancamentoCredito = await InserirCredito(lancamento.Conta.Id, descricao, lancamento.Valor);

            lancamento.SetStatus(LancamentoStatusEnum.Cancelado);

            await lancamentoRepositorio.Editar(lancamento);

            return lancamentoCredito;
        }
    }
}
