using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Repositorios
{
    public interface IAuditoriaRepositorio
    {
        Task<IList<Auditoria>> Recuperar(int pagina, int registroPorPagina, string sortBy, string order, Expression<Func<Auditoria, bool>> filtros);
        Task<long> RecuperarTotal(Expression<Func<Auditoria, bool>> filtros);
        Task<Auditoria> Recuperar(Expression<Func<Auditoria, bool>> filtros);
        Task<Auditoria> RecuperarDetalhe(int id);
        Task Inserir(Auditoria auditoria);
        Task Editar(Auditoria auditoria);
        Task Excluir(Auditoria auditoria);
    }
}
