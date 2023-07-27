using FluxoCaixa.Projeto.Biblioteca.Filtros;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Repositorios;
using FluxoCaixa.Projeto.Infraestrutura.RepositorioBase;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Infraestrutura.Caixa.Lancamentos.Repositorios
{
    public class LancamentoRepositorioAsync : RepositoryAsync<Lancamento>, ILancamentoRepositorio
    {
        public LancamentoRepositorioAsync(ISession _session) : base(_session) { }

        public async Task<IList<Lancamento>> Recuperar(int pagina, int registroPorPagina, string sortBy, string order, Expression<Func<Lancamento, bool>> filtros)
        {
            if (pagina != 0) { pagina = pagina - 1; }

            return await GetAll().Where(filtros).Skip(pagina * registroPorPagina).Take(registroPorPagina).OrderBy(sortBy + " " + order).ToListAsync();
        }

        public async Task<long> RecuperarTotal(Expression<Func<Lancamento, bool>> filtros)
        {
            return await GetAll().Where(filtros).CountAsync();
        }

        public async Task<Lancamento> Recuperar(Expression<Func<Lancamento, bool>> filtros)
        {
            return await GetAll().Where(filtros).SingleOrDefaultAsync();
        }

        public async Task<Lancamento> RecuperarDetalhe(int id)
        {
            return await GetAll().Where(p => p.Id == id).SingleOrDefaultAsync();
        }

        public async Task<Lancamento> RecuperarDetalhe(Expression<Func<Lancamento, bool>> filtros)
        {
            return await GetAll().Where(filtros).SingleOrDefaultAsync();
        }

        public async Task Inserir(Lancamento lancamento)
        {
            await Create(lancamento);
        }

        public async Task Editar(Lancamento lancamento)
        {
            await Update(lancamento);
        }

        public async Task Excluir(Lancamento lancamento)
        {
            await Delete(lancamento.Id);
        }
    }
}
