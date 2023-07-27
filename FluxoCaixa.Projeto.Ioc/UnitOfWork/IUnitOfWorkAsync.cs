using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Ioc.UnitOfWork
{
    public interface IUnitOfWorkAsync
    {
        void BeginTransaction();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
