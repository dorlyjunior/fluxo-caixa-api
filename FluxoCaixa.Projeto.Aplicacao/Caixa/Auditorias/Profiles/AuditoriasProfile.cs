using AutoMapper;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Auditorias.Responses;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Entidades;

namespace FluxoCaixa.Projeto.Aplicacao.Caixa.Auditorias.Profiles
{
    public class AuditoriasProfile : Profile
    {
        public AuditoriasProfile()
        {
            CreateMap<Auditoria, AuditoriaResponse>();
        }
    }
}
