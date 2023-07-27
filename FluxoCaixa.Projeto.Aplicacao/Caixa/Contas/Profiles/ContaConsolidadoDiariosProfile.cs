using AutoMapper;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Responses;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;

namespace FluxoCaixa.Projeto.Aplicacao.Caixa.Contas.Profiles
{
    public class ContaConsolidadoDiariosProfile : Profile
    {
        public ContaConsolidadoDiariosProfile()
        {
            CreateMap<ContaConsolidadoDiario, ContaConsolidadoDiarioResponse>()
                .ForMember(
                    dest => dest.CodigoStatus,
                    opt => opt.MapFrom(prop => (int)prop.Status)
                )
                ;
        }
    }
}
