using FizzWare.NBuilder;
using FluentAssertions;
using FluxoCaixa.Projeto.Biblioteca.Enumeradores;
using FluxoCaixa.Projeto.Biblioteca.Excecoes;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Repositorios;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Servicos;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace FluxoCaixa.Projeto.Testes.Caixa.Contas.Servicos
{
    public class ContasServicoTestes
    {
        private readonly ContasServico sut;
        private readonly Conta contaValida;
        private readonly IContaRepositorio contaRepositorio;

        public ContasServicoTestes()
        {
            contaValida = Builder<Conta>.CreateNew().Build();
            contaRepositorio = Substitute.For<IContaRepositorio>();

            sut = new ContasServico(contaRepositorio);
        }

        public class ValidarMetodo : ContasServicoTestes
        {
            [Fact]
            public void Dado_ContaNaoEncontrada_Espero_RegraDeNegocioExcecao()
            {
                contaRepositorio.RecuperarDetalhe(Arg.Any<int>()).ReturnsNull();

                sut.Invoking(p => p.Validar(1)).Should().ThrowAsync<RegraDeNegocioExcecao>();
            }

            [Fact]
            public void Dado_ContaEncontrada_Espero_ContaValida()
            {
                contaRepositorio.RecuperarDetalhe(Arg.Any<int>()).Returns(contaValida);

                sut.Validar(1).Result.Should().BeSameAs(contaValida);
            }
        }

        public class InserirMetodo : ContasServicoTestes
        {
            [Fact]
            public void Dado_ParametrosValidos_Espero_ContaInserida()
            {
                // Arrange
                var contaInserida = new Conta
                {
                    Id = 0,
                    Nome = "***",
                    Status = AtivoInativoEnum.Ativo
                };

                // Act
                contaRepositorio.Inserir(Arg.Any<Conta>()).Returns(Task.FromResult(contaInserida));

                // Assert
                sut.Inserir("***").Result.Should().BeEquivalentTo(contaInserida);
            }

            [Fact]
            public async Task Dado_ParametrosInvalidos_Espero_RegraDeNegocioExcecao()
            {
                await sut.Invoking(p => p.Inserir(null)).Should().ThrowAsync<RegraDeNegocioExcecao>();
            }
        }

        public class AtualizarMetodo : ContasServicoTestes
        {
            [Fact]
            public void Dado_ParametrosValidos_Espero_ContaAtualizada()
            {
                var contaEditada = new Conta { Id = 1, Nome = "***", Status = AtivoInativoEnum.Ativo, Saldo = 1 };

                sut.Validar(Arg.Any<int>()).Returns(Task.FromResult(contaValida));

                contaRepositorio.Editar(Arg.Any<Conta>()).Returns(Task.FromResult(contaEditada));

                sut.Atualizar(1, "***").Result.Should().BeEquivalentTo(contaEditada);
            }

            [Fact]
            public async Task Dado_IdDaContaInvalida_Espero_RegraDeNegocioExcecao()
            {
                sut.Validar(Arg.Any<int>()).ThrowsAsync(new RegraDeNegocioExcecao("***"));

                await sut.Invoking(p => p.Atualizar(1, "***")).Should().ThrowAsync<RegraDeNegocioExcecao>();
            }
        }

        public class IncrementarSaldoMetodo : ContasServicoTestes
        {
            [Fact]
            public void Dado_ValorValido_Espero_SaldoIncrementado()
            {
                var valor = 100;
                var contaEditada = new Conta { Id = 1, Nome = "***", Status = AtivoInativoEnum.Inativo };
                contaEditada.SetSaldo(valor);

                contaRepositorio.Editar(Arg.Any<Conta>()).Returns(Task.FromResult(contaEditada));

                sut.IncrementarSaldo(contaValida, valor).Result.Saldo.Should().Be(101);
            }
        }
        
        public class DecrementarSaldoMetodo : ContasServicoTestes
        {
            [Fact]
            public void Dado_ValorValido_Espero_SaldoDecrementado()
            {
                var valor = 1;
                var contaEditada = new Conta { Id = 1, Nome = "***", Status = AtivoInativoEnum.Ativo };
                contaEditada.SetSaldo(valor);

                contaRepositorio.Editar(Arg.Any<Conta>()).Returns(Task.FromResult(contaEditada));

                sut.DecrementarSaldo(contaValida, valor).Result.Saldo.Should().Be(0);
            }
            
            [Fact]
            public async Task Dado_ValorMaiorQueSaldo_Espero_RegraDeNegocioExcecao()
            {
                var valor = 200;

                await sut.Invoking(p => p.DecrementarSaldo(contaValida, valor)).Should().ThrowAsync<RegraDeNegocioExcecao>();
            }
        }
        
        public class InativarMetodo : ContasServicoTestes
        {
            [Fact]
            public void Dado_ContaValida_Espero_StatusInativo()
            {
                var contaEditada = new Conta { Id = 1, Nome = "***", Status = AtivoInativoEnum.Inativo };

                sut.Validar(Arg.Any<int>()).Returns(Task.FromResult(contaValida));

                contaRepositorio.Editar(Arg.Any<Conta>()).Returns(Task.FromResult(contaEditada));

                sut.Inativar(1).Result.Status.Should().Be(AtivoInativoEnum.Inativo);
            }
            
            [Fact]
            public async Task Dado_ContaInvalida_Espero_RegraDeNegocioExcecao()
            {
                sut.Validar(Arg.Any<int>()).ThrowsAsync(new RegraDeNegocioExcecao("***"));

                await sut.Invoking(p => p.Inativar(1)).Should().ThrowAsync<RegraDeNegocioExcecao>();
            }
        }
    }
}
