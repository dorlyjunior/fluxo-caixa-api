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

namespace FluxoCaixa.Projeto.Infraestrutura.Caixa.Contas.Repositorios
{
    public class ContaRepositorioAsync : RepositoryAsync<Conta>, IContaRepositorio
    {
        public ContaRepositorioAsync(ISession _session) : base(_session) { }

        public async Task<IList<Conta>> Recuperar(int pagina, int registroPorPagina, string sortBy, string order, Expression<Func<Conta, bool>> filtros)
        {
            if (pagina != 0) { pagina = pagina - 1; }

            return await GetAll().Where(filtros).Skip(pagina * registroPorPagina).Take(registroPorPagina).OrderBy(sortBy + " " + order).ToListAsync();
        }

        public async Task<long> RecuperarTotal(Expression<Func<Conta, bool>> filtros)
        {
            return await GetAll().Where(filtros).CountAsync();
        }

        public async Task<Conta> Recuperar(Expression<Func<Conta, bool>> filtros)
        {
            return await GetAll().Where(filtros).SingleOrDefaultAsync();
        }

        public async Task<Conta> RecuperarDetalhe(int id)
        {
            return await GetAll().Where(p => p.Id == id).SingleOrDefaultAsync();
        }

        public async Task Inserir(Conta conta)
        {
            await Create(conta);
        }

        public async Task Editar(Conta conta)
        {
            await Update(conta);
        }

        public async Task Excluir(Conta conta)
        {
            await Delete(conta.Id);
        }
    }
}
