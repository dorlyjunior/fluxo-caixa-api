using FluxoCaixa.Projeto.Biblioteca.Enumeradores;
using FluxoCaixa.Projeto.Biblioteca.Excecoes;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Repositorios;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Servicos.Interfaces;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Contas.Servicos
{
    public class ContasServico : IContasServico
    {
        private readonly IContaRepositorio contaRepositorio;
        
        public ContasServico(IContaRepositorio contaRepositorio)
        {
            this.contaRepositorio = contaRepositorio;
        }

        public async Task<Conta> Validar(int id)
        {
            var conta = await contaRepositorio.RecuperarDetalhe(id);

            if (conta == null)
            {
                throw new RegraDeNegocioExcecao("Conta não encontrada.");
            }

            return conta;
        }

        public async Task<Conta> Inserir(string nome)
        {
            var conta = new Conta(nome);

            await contaRepositorio.Inserir(conta);

            return conta;
        }


        public async Task<Conta> Atualizar(int id, string nome)
        {
            var conta = await Validar(id);

            conta.SetNome(nome);

            await contaRepositorio.Editar(conta);

            return conta;
        }

        public async Task<Conta> IncrementarSaldo(Conta conta, decimal saldo)
        {
            conta.IncrementarSaldo(saldo);

            await contaRepositorio.Editar(conta);

            return conta;
        }
        
        public async Task<Conta> DecrementarSaldo(Conta conta, decimal saldo)
        {
            conta.DecrementarSaldo(saldo);

            await contaRepositorio.Editar(conta);

            return conta;
        }

        public async Task<Conta> Inativar(int id)
        {
            var conta = await Validar(id);

            conta.SetStatus(AtivoInativoEnum.Inativo);

            await contaRepositorio.Editar(conta);

            return conta;
        }
    }
}
