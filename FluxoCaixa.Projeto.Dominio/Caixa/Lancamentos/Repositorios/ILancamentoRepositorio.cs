using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Repositorios
{
    public interface ILancamentoRepositorio
    {
        Task<IList<Lancamento>> Recuperar(int pagina, int registroPorPagina, string sortBy, string order, Expression<Func<Lancamento, bool>> filtros);
        Task<long> RecuperarTotal(Expression<Func<Lancamento, bool>> filtros);
        Task<Lancamento> Recuperar(Expression<Func<Lancamento, bool>> filtros);
        Task<Lancamento> RecuperarDetalhe(int id);
        Task Inserir(Lancamento lancamento);
        Task Editar(Lancamento lancamento);
        Task Excluir(Lancamento lancamento);
    }
}
