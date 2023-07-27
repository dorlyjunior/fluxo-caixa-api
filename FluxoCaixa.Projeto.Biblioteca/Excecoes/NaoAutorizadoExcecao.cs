using System;

namespace FluxoCaixa.Projeto.Biblioteca.Excecoes
{
    public class NaoAutorizadoExcecao : Exception
    {
        public NaoAutorizadoExcecao(string mensagem) : base(mensagem) { }
    }
}
