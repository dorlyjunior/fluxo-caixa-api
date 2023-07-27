using AutoMapper;
using FluxoCaixa.Projeto.Aplicacao.Caixa.Auditorias.Servicos.Interfaces;
using FluxoCaixa.Projeto.Biblioteca.Filtros;
using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Auditorias.Requests;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Auditorias.Responses;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Repositorios;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Servicos.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Aplicacao.Caixa.Auditorias.Servicos
{
    public class AuditoriasAppServico : IAuditoriasAppServico
    {
        private readonly IAuditoriaRepositorio auditoriaRepositorio;
        private readonly IMapper mapper;
        private readonly ILogger<AuditoriasAppServico> logger;

        public AuditoriasAppServico(
            IAuditoriaRepositorio auditoriaRepositorio,
            IMapper mapper,
            ILogger<AuditoriasAppServico> logger
        )
        {
            this.auditoriaRepositorio = auditoriaRepositorio;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<PaginacaoResponse<AuditoriaResponse>> Recuperar(AuditoriaListagemRequest request)
        {
            Expression<Func<Auditoria, bool>> filtros = PredicateBuilder.True<Auditoria>();

            request = request ?? new AuditoriaListagemRequest();

            if (request.Id != 0)
            {
                filtros = filtros.And(p => p.Id == request.Id);
            }

            if (!string.IsNullOrWhiteSpace(request.Usuario))
            {
                filtros = filtros.And(p => p.Usuario.Contains(request.Usuario));
            }

            if (request.DataAcaoDe != DateTime.MinValue)
            {
                filtros = filtros.And(p => p.DataAcao >= request.DataAcaoDe);
            }

            if (request.DataAcaoAte != DateTime.MinValue)
            {
                filtros = filtros.And(p => p.DataAcao <= request.DataAcaoAte);
            }

            IList<Auditoria> conta = await auditoriaRepositorio.Recuperar(request.Pg, request.Ps, request.SortBy, request.Order, filtros);
            long total = await auditoriaRepositorio.RecuperarTotal(filtros);

            var response = new PaginacaoResponse<AuditoriaResponse>()
            {
                PageIndex = request.Pg,
                PageSize = request.Ps,
                Total = total,
                List = mapper.Map<IList<AuditoriaResponse>>(conta)
            };

            logger.LogInformation("Uma pesquisa a listagem de auditorias foi realizada");

            return response;
        }
    }
}
