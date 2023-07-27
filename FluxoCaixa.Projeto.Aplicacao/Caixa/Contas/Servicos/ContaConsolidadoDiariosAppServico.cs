using AutoMapper;
using FluxoCaixa.Projeto.Aplicacao.Caixa.Contas.Servicos.Interfaces;
using FluxoCaixa.Projeto.Biblioteca.Filtros;
using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using FluxoCaixa.Projeto.Biblioteca.Sessao;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Requests;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Responses;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Servicos;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Servicos.Interfaces;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Enumeradores;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Repositorios;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Aplicacao.Caixa.Contas.Servicos
{
    public class ContaConsolidadoDiariosAppServico : IContaConsolidadoDiariosAppServico
    {
        private readonly IContaConsolidadoDiarioRepositorio consolidadoRepositorio;
        private readonly IContaConsolidadoDiariosServico consolidadosServico;
        private readonly IMapper mapper;
        private readonly ISessao sessao;
        private readonly IAuditoriasServico auditoriasServico;

        public ContaConsolidadoDiariosAppServico(
            IContaConsolidadoDiarioRepositorio consolidadoRepositorio,
            IContaConsolidadoDiariosServico consolidadosServico,
            IMapper mapper,
            ISessao sessao,
            IAuditoriasServico auditoriasServico
        )
        {
            this.consolidadoRepositorio = consolidadoRepositorio;
            this.consolidadosServico = consolidadosServico;
            this.mapper = mapper;
            this.sessao = sessao;
            this.auditoriasServico = auditoriasServico;
        }

        public async Task<PaginacaoResponse<ContaConsolidadoDiarioResponse>> Recuperar(ContaConsolidadoDiarioListagemRequest request)
        {
            Expression<Func<ContaConsolidadoDiario, bool>> filtros = PredicateBuilder.True<ContaConsolidadoDiario>();

            request = request ?? new ContaConsolidadoDiarioListagemRequest();

            if (request.Id != 0)
            {
                filtros = filtros.And(p => p.Id == request.Id);
            }

            if (request.DataDia != DateTime.MinValue)
            {
                filtros = filtros.And(p => p.DataDia == request.DataDia);
            }

            if (request.Dia != 0)
            {
                filtros = filtros.And(p => p.Dia == request.Dia);
            }
            
            if (request.Mes != 0)
            {
                filtros = filtros.And(p => p.Mes == request.Mes);
            }
            
            if (request.Ano != 0)
            {
                filtros = filtros.And(p => p.Ano == request.Ano);
            }

            if (!string.IsNullOrWhiteSpace(request.DiaSemana))
            {
                filtros = filtros.And(p => p.DiaSemana.Contains(request.DiaSemana));
            }

            if (request.DataConsolidacaoDe != DateTime.MinValue)
            {
                filtros = filtros.And(p => p.DataConsolidacao >= request.DataConsolidacaoDe);
            }
            
            if (request.DataConsolidacaoAte != DateTime.MinValue)
            {
                filtros = filtros.And(p => p.DataConsolidacao <= request.DataConsolidacaoAte);
            }

            if (request.CodigoStatus != 0)
            {
                filtros = filtros.And(p => p.Status == (ContaConsolidadoDiarioStatusEnum)request.CodigoStatus);
            }

            IList<ContaConsolidadoDiario> conta = await consolidadoRepositorio.Recuperar(request.Pg, request.Ps, request.SortBy, request.Order, filtros);
            long total = await consolidadoRepositorio.RecuperarTotal(filtros);

            var response = new PaginacaoResponse<ContaConsolidadoDiarioResponse>()
            {
                PageIndex = request.Pg,
                PageSize = request.Ps,
                Total = total,
                List = mapper.Map<IList<ContaConsolidadoDiarioResponse>>(conta)
            };

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Pesquisou informações de consolidado diário");

            return response;
        }

        public async Task<ContaConsolidadoDiarioResponse> Recuperar(int id, string dia)
        {
            ContaConsolidadoDiario conta = await consolidadosServico.Validar(id, dia);

            var response = mapper.Map<ContaConsolidadoDiarioResponse>(conta);

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Recuperou um detalhe de consolidado diário");

            return response;
        }
        
        public async Task<ContaConsolidadoDiarioResponse> ConsolidarDia(int id, string dia)
        {
            ContaConsolidadoDiario conta = await consolidadosServico.Consolidar(id, dia);

            var response = mapper.Map<ContaConsolidadoDiarioResponse>(conta);

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Consolidou os lançamentos de um dia");

            return response;
        }
    }
}
