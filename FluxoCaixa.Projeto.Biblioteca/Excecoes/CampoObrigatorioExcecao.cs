namespace FluxoCaixa.Projeto.Biblioteca.Excecoes
{
    public class CampoObrigatorioExcecao : RegraDeNegocioExcecao
    {
        public CampoObrigatorioExcecao(string campo) : base("O campo " + campo + " é obrigatório.")
        {
        }
    }
}
