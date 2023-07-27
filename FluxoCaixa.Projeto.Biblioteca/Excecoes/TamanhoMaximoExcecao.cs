using System;

namespace FluxoCaixa.Projeto.Biblioteca.Excecoes
{
    public class TamanhoMaximoExcecao : RegraDeNegocioExcecao
    {
        public TamanhoMaximoExcecao(string campo, int tamanho) : base("O campo " + campo + " excedeu o limite de caracteres de " + tamanho) { }
    }
}
