using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Repositorios;
using FluxoCaixa.Projeto.Infraestrutura.RepositorioBase;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Infraestrutura.Caixa.Auditorias.Repositorios
{
    public class AuditoriaRepositorioAsync : RepositoryAsync<Auditoria>, IAuditoriaRepositorio
    {
        public AuditoriaRepositorioAsync(ISession _session) : base(_session) { }

        public async Task<IList<Auditoria>> Recuperar(int pagina, int registroPorPagina, string sortBy, string order, Expression<Func<Auditoria, bool>> filtros)
        {
            if (pagina != 0) { pagina = pagina - 1; }

            return await GetAll().Where(filtros).Skip(pagina * registroPorPagina).Take(registroPorPagina).OrderBy(sortBy + " " + order).ToListAsync();
        }

        public async Task<long> RecuperarTotal(Expression<Func<Auditoria, bool>> filtros)
        {
            return await GetAll().Where(filtros).CountAsync();
        }

        public async Task<Auditoria> Recuperar(Expression<Func<Auditoria, bool>> filtros)
        {
            return await GetAll().Where(filtros).SingleOrDefaultAsync();
        }

        public async Task<Auditoria> RecuperarDetalhe(int id)
        {
            return await GetAll().Where(p => p.Id == id).SingleOrDefaultAsync();
        }

        public async Task Inserir(Auditoria auditoria)
        {
            await Create(auditoria);
        }

        public async Task Editar(Auditoria auditoria)
        {
            await Update(auditoria);
        }

        public async Task Excluir(Auditoria auditoria)
        {
            await Delete(auditoria.Id);
        }
    }
}
