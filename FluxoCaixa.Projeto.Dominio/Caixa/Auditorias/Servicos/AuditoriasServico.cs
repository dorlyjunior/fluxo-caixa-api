using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Repositorios;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Servicos.Interfaces;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Servicos
{
    public class AuditoriasServico : IAuditoriasServico
    {
        private readonly IAuditoriaRepositorio auditoriaRepositorio;

        public AuditoriasServico(IAuditoriaRepositorio auditoriaRepositorio)
        {
            this.auditoriaRepositorio = auditoriaRepositorio;
        }

        public async Task<Auditoria> Inserir(string usuario, string acao)
        {
            var auditoria = new Auditoria(usuario, acao);

            await auditoriaRepositorio.Inserir(auditoria);

            return auditoria;
        }
    }
}
