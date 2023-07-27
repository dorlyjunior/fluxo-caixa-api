using AutoMapper;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Lancamentos.Responses;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Entidades;

namespace FluxoCaixa.Projeto.Aplicacao.Caixa.Lancamentos.Profiles
{
    public class LancamentoProfile : Profile
    {
        public LancamentoProfile()
        {
            CreateMap<Lancamento, LancamentoResponse>()
                .ForMember(
                    dest => dest.CodigoStatus,
                    opt => opt.MapFrom(prop => (int)prop.Status)
                )
                .ForMember(
                    dest => dest.CodigoTipo,
                    opt => opt.MapFrom(prop => (int)prop.Tipo)
                )
                ;
        }
    }
}
