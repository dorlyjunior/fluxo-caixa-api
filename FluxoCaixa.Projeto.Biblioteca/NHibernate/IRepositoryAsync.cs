using System.Linq;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Biblioteca.NHibernate
{
    public interface IRepositoryAsync<T> where T : IEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetById(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
