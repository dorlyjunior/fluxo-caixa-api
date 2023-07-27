namespace FluxoCaixa.Projeto.Biblioteca.Sessao
{
    public interface ISessao
    {
        string Id { get; }
        string Token { get; }
        bool IsAuthenticated();
        string GetInfo(string info);
        string GetNomeDoUsuario();
    }
}
