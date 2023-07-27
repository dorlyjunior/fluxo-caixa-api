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
    }
}
