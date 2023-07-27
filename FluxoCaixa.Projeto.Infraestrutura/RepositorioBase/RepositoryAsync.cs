using FluxoCaixa.Projeto.Biblioteca.NHibernate;
using NHibernate;
using System.Linq;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Infraestrutura.RepositorioBase
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : IEntity
    {
        protected ISession Session;

        public RepositoryAsync(ISession _session)
        {
            Session = _session;
        }

        public IQueryable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public async Task<T> GetById(int id)
        {
            return await Session.GetAsync<T>(id);
        }

        public async Task Create(T entity)
        {
            await Session.SaveAsync(entity);
        }

        public async Task Update(T entity)
        {
            await Session.UpdateAsync(entity);
        }

        public async Task Delete(int id)
        {
            await Session.DeleteAsync(Session.Load<T>(id));
        }
    }
}
