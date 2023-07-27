using NHibernate;
using System;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Ioc.UnitOfWork
{
    public class UnitOfWorkAsync : IUnitOfWorkAsync
    {
        private ITransaction Transaction;
        public ISession Session { get; set; }

        public UnitOfWorkAsync() { }

        public UnitOfWorkAsync(ISession session)
        {
            Session = session;
        }

        public void BeginTransaction()
        {
            Transaction = Session.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            try
            {
                if (Transaction != null && Transaction.IsActive)
                {
                    await Transaction.CommitAsync();
                }
            }
            catch (Exception)
            {
                if (Transaction != null && Transaction.IsActive)
                {
                    await Transaction.RollbackAsync();
                }

                throw;
            }
            finally
            {
                Session.Dispose();
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                if (Transaction != null && Transaction.IsActive)
                {
                    await Transaction.RollbackAsync();
                }
            }
            finally
            {
                Session.Dispose();
            }
        }
    }
}
