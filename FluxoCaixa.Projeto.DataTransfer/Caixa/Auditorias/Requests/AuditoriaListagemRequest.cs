using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using System;

namespace FluxoCaixa.Projeto.DataTransfer.Caixa.Auditorias.Requests
{
    public class AuditoriaListagemRequest : PaginacaoRequest
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime DataAcaoDe { get; set; }
        public DateTime DataAcaoAte { get; set; }
    }
}
