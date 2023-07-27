using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Contas.Repositorios
{
    public interface IContaRepositorio
    {
        Task<IList<Conta>> Recuperar(int pagina, int registroPorPagina, string sortBy, string order, Expression<Func<Conta, bool>> filtros);
        Task<long> RecuperarTotal(Expression<Func<Conta, bool>> filtros);
        Task<Conta> Recuperar(Expression<Func<Conta, bool>> filtros);
        Task<Conta> RecuperarDetalhe(int id);
        Task Inserir(Conta conta);
        Task Editar(Conta conta);
        Task Excluir(Conta conta);
    }
}
