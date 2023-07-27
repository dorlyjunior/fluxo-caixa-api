using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Entidades;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Servicos.Interfaces
{
    public interface IAuditoriasServico
    {
        Task<Auditoria> Inserir(string usuario, string acao);
    }
}
