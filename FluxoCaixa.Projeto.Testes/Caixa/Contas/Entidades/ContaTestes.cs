using FizzWare.NBuilder;
using FluentAssertions;
using FluxoCaixa.Projeto.Biblioteca.Enumeradores;
using FluxoCaixa.Projeto.Biblioteca.Excecoes;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FluxoCaixa.Projeto.Testes.Caixa.Contas.Entidades
{
    public class ContaTestes
    {
        private readonly Conta sut;
        private readonly string NOME_CONTA = "Caixa 01";

        public ContaTestes()
        {
            sut = Builder<Conta>.CreateNew().Build();
        }

        public class Constructor : ContaTestes
        {
            [Fact]
            public void Quando_ParametrosValidos_Espero_ObjetoIntegro()
            {
                var sut = new Conta(NOME_CONTA);

                Assert.NotNull(sut);
            }

            [Fact]
            public void Quando_ParametrosValidos_Espero_NomeValido()
            {
                var sut = new Conta(NOME_CONTA);

                sut.Nome.Should().Be(NOME_CONTA);
            }

            [Fact]
            public void Quando_ParametrosValidos_Espero_StatusAtivo()
            {
                var sut = new Conta(NOME_CONTA);

                sut.Status.Should().Be(AtivoInativoEnum.Ativo);
            }
            
            [Fact]
            public void Quando_ParametrosValidos_Espero_SaldoZerado()
            {
                var sut = new Conta(NOME_CONTA);

                sut.Saldo.Should().Be(0);
            }
        }

        public class SetNomeMetodo : ContaTestes
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void Dado_NomeNuloOuEspacoEmBranco_Espero_AtributoObrigatorioExcecao(string nome)
            {
                sut.Invoking(p => p.SetNome(nome)).Should().Throw<CampoObrigatorioExcecao>();
            }

            [Fact]
            public void Dado_NomeComMaisDe200Caracteres_Espero_TamanhoMaximoExcecao()
            {
                var nome = new string('*', 201);

                sut.Invoking(p => p.SetNome(nome)).Should().Throw<TamanhoMaximoExcecao>();
            }
        }

        public class SetSaldoMetodo : ContaTestes
        {
            [Fact]
            public void Dado_ValorMenorQueZero_Espero_AtributoObrigatorioExcecao()
            {
                decimal valor = -50; 

                sut.Invoking(p => p.SetSaldo(valor)).Should().Throw<RegraDeNegocioExcecao>();
            }

            [Theory]
            [InlineData(0)]
            [InlineData(10)]
            [InlineData(100)]
            public void Dado_ValorMaiorQueZero_Espero_SaldoValido(int valor)
            {
                sut.Invoking(p => p.SetSaldo(valor)).Should().NotThrow<RegraDeNegocioExcecao>();
            }
        }

        public class SetIncrementarSaldoMetodo : ContaTestes
        {
            [Fact]
            public void Dado_Valor_Espero_SaldoIncrementado()
            {
                decimal valor = 100;
                decimal saldo = 100;

                sut.SetSaldo(saldo);
                sut.IncrementarSaldo(valor);

                sut.Saldo.Should().Be(saldo+valor);
            }
        }

        public class SetDecrementarSaldoMetodo : ContaTestes
        {
            [Fact]
            public void Dado_Valor_Espero_SaldoIncrementado()
            {
                decimal saldo = 100;
                decimal valor = 100;

                sut.SetSaldo(saldo);
                sut.DecrementarSaldo(valor);

                sut.Saldo.Should().Be(0);
            }

            [Fact]
            public void Dado_ValorMaiorQueSaldo_Espero_ErroDeSaudoInsuficiente()
            {
                decimal saldo = 100;
                decimal valor = 200;

                sut.SetSaldo(saldo);

                sut.Invoking(p => p.DecrementarSaldo(valor)).Should().Throw<RegraDeNegocioExcecao>();
            }
        }

        public class SetStatusMetodo : ContaTestes
        {
            [Theory]
            [InlineData(-1)]
            [InlineData(0)]
            [InlineData(4)]
            public void Dado_StatusValorMenorQueZeroOuMaiorQueTres_Espero_RegraDeNegocioExcecao(int status)
            {
                sut.Invoking(p => p.SetStatus(status)).Should().Throw<RegraDeNegocioExcecao>();
            }

            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            public void Dado_StatusValorEntreUmETres_Espero_NenhumaExcecao(int status)
            {
                sut.Invoking(p => p.SetStatus(status)).Should().NotThrow<RegraDeNegocioExcecao>();
            }


            [Fact]
            public void Dado_StatusAtivo_Espero_RegistroAtivo()
            {
                var status = AtivoInativoEnum.Ativo;

                sut.SetStatus(status);

                sut.Status.Should().Be(AtivoInativoEnum.Ativo);
            }

            [Fact]
            public void Dado_StatusInativo_Espero_RegistroInativo()
            {
                var status = AtivoInativoEnum.Inativo;

                sut.SetStatus(status);

                sut.Status.Should().Be(AtivoInativoEnum.Inativo);
            }

            [Fact]
            public void Dado_StatusExcluido_Espero_RegistroExcluido()
            {
                var status = AtivoInativoEnum.Excluido;

                sut.SetStatus(status);

                sut.Status.Should().Be(AtivoInativoEnum.Excluido);
            }
        }

        public class VerificaSeEstaAtivaMetodo : ContaTestes
        {
            [Fact]
            public void Dado_ContaInativa_Espero_Verdadeiro()
            {
                var status = AtivoInativoEnum.Inativo;

                sut.SetStatus(status);

                sut.VerificaSeEstaInativa().Should().BeTrue();
            }

            [Fact]
            public void Dado_ContaAtiva_Espero_Falso()
            {
                var status = AtivoInativoEnum.Ativo;

                sut.SetStatus(status);

                sut.VerificaSeEstaInativa().Should().BeFalse();
            }
        }
    }
}
