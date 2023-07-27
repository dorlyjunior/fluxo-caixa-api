using System;

namespace FluxoCaixa.Projeto.Biblioteca.Excecoes
{
    public class RegraDeNegocioExcecao : Exception
    {
        public RegraDeNegocioExcecao(string mensagem) : base(mensagem) { }
    }
}
