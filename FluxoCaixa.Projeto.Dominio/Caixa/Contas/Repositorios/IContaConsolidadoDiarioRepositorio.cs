using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Contas.Repositorios
{
    public interface IContaConsolidadoDiarioRepositorio
    {
        Task<IList<ContaConsolidadoDiario>> Recuperar(int pagina, int registroPorPagina, string sortBy, string order, Expression<Func<ContaConsolidadoDiario, bool>> filtros);
        Task<long> RecuperarTotal(Expression<Func<ContaConsolidadoDiario, bool>> filtros);
        Task<ContaConsolidadoDiario> Recuperar(Expression<Func<ContaConsolidadoDiario, bool>> filtros);
        Task<ContaConsolidadoDiario> RecuperarDetalhe(int id);
        Task Inserir(ContaConsolidadoDiario consolidado);
        Task Editar(ContaConsolidadoDiario consolidado);
        Task Excluir(ContaConsolidadoDiario consolidado);
    }
}
