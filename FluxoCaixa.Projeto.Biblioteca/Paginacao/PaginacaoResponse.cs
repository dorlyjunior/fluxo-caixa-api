using System.Collections.Generic;

namespace FluxoCaixa.Projeto.Biblioteca.Paginacao
{
    public class PaginacaoResponse<T>
    {
        public int PageSize { get; set; }
        public long Total { get; set; }
        public int PageIndex { get; set; }
        public IList<T> List { get; set; }
    }
}
