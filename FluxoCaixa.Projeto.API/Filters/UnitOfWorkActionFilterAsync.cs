using FluxoCaixa.Projeto.Ioc.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.API.Filters
{
    public class UnitOfWorkActionFilterAsync : ActionFilterAttribute
    {
        public IUnitOfWorkAsync UnitOfWorkAsync { get; set; }

        public UnitOfWorkActionFilterAsync(IUnitOfWorkAsync _unitOfWork)
        {
            UnitOfWorkAsync = _unitOfWork;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            UnitOfWorkAsync.BeginTransaction();

            ActionExecutedContext executado = await next.Invoke();

            if (executado.Exception == null)
            {
                await UnitOfWorkAsync.CommitAsync();
            }
            else
            {
                await UnitOfWorkAsync.RollbackAsync();
            }
        }
    }
}
