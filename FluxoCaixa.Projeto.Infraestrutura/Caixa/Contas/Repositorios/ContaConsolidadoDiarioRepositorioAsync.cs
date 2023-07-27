using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Repositorios;
using FluxoCaixa.Projeto.Infraestrutura.RepositorioBase;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Infraestrutura.Caixa.ContaConsolidadoDiarios.Repositorios
{
    public class ContaConsolidadoDiarioRepositorioAsync : RepositoryAsync<ContaConsolidadoDiario>, IContaConsolidadoDiarioRepositorio
    {
        public ContaConsolidadoDiarioRepositorioAsync(ISession _session) : base(_session) { }

        public async Task<IList<ContaConsolidadoDiario>> Recuperar(int pagina, int registroPorPagina, string sortBy, string order, Expression<Func<ContaConsolidadoDiario, bool>> filtros)
        {
            if (pagina != 0) { pagina = pagina - 1; }

            return await GetAll().Where(filtros).Skip(pagina * registroPorPagina).Take(registroPorPagina).OrderBy(sortBy + " " + order).ToListAsync();
        }

        public async Task<long> RecuperarTotal(Expression<Func<ContaConsolidadoDiario, bool>> filtros)
        {
            return await GetAll().Where(filtros).CountAsync();
        }

        public async Task<ContaConsolidadoDiario> Recuperar(Expression<Func<ContaConsolidadoDiario, bool>> filtros)
        {
            return await GetAll().Where(filtros).SingleOrDefaultAsync();
        }

        public async Task<ContaConsolidadoDiario> RecuperarDetalhe(Expression<Func<ContaConsolidadoDiario, bool>> filtros)
        {
            return await GetAll().Where(filtros).SingleOrDefaultAsync();
        }

        public async Task<ContaConsolidadoDiario> RecuperarDetalhe(int id)
        {
            return await GetAll().Where(p => p.Id == id).SingleOrDefaultAsync();
        }

        public async Task Inserir(ContaConsolidadoDiario conta)
        {
            await Create(conta);
        }

        public async Task Editar(ContaConsolidadoDiario conta)
        {
            await Update(conta);
        }

        public async Task Excluir(ContaConsolidadoDiario conta)
        {
            await Delete(conta.Id);
        }
    }
}
