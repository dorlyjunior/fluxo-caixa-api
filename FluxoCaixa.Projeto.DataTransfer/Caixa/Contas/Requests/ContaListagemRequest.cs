using FluxoCaixa.Projeto.Biblioteca.Paginacao;

namespace FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Requests
{
    public class ContaListagemRequest : PaginacaoRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CodigoStatus { get; set; }
    }
}
