using AutoMapper;
using FluxoCaixa.Projeto.Aplicacao.Caixa.Contas.Servicos.Interfaces;
using FluxoCaixa.Projeto.Biblioteca.Enumeradores;
using FluxoCaixa.Projeto.Biblioteca.Filtros;
using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using FluxoCaixa.Projeto.Biblioteca.Sessao;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Requests;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Responses;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Servicos.Interfaces;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Repositorios;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Servicos.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Aplicacao.Caixa.Contas.Servicos
{
    public class ContasAppServico : IContasAppServico
    {
        private readonly IContaRepositorio contaRepositorio;
        private readonly IContasServico contasServico;
        private readonly IMapper mapper;
        private readonly ISessao sessao;
        private readonly IAuditoriasServico auditoriasServico;
        private readonly ILogger<ContasAppServico> logger;

        public ContasAppServico(
            IContaRepositorio contaRepositorio,
            IContasServico contasServico,
            IMapper mapper,
            ISessao sessao,
            IAuditoriasServico auditoriasServico,
            ILogger<ContasAppServico> logger
        )
        {
            this.contaRepositorio = contaRepositorio;
            this.contasServico = contasServico;
            this.mapper = mapper;
            this.sessao = sessao;
            this.auditoriasServico = auditoriasServico;
            this.logger = logger;
        }

        public async Task<PaginacaoResponse<ContaResponse>> Recuperar(ContaListagemRequest request)
        {
            Expression<Func<Conta, bool>> filtros = PredicateBuilder.True<Conta>();

            request = request ?? new ContaListagemRequest();

            if (request.Id != 0)
            {
                filtros = filtros.And(p => p.Id == request.Id);
            }

            if (request.CodigoStatus != 0)
            {
                filtros = filtros.And(p => p.Status == (AtivoInativoEnum)request.CodigoStatus);
            }

            if (!string.IsNullOrWhiteSpace(request.Nome))
            {
                filtros = filtros.And(p => p.Nome.Contains(request.Nome));
            }

            IList<Conta> conta = await contaRepositorio.Recuperar(request.Pg, request.Ps, request.SortBy, request.Order, filtros);
            long total = await contaRepositorio.RecuperarTotal(filtros);

            var response = new PaginacaoResponse<ContaResponse>()
            {
                PageIndex = request.Pg,
                PageSize = request.Ps,
                Total = total,
                List = mapper.Map<IList<ContaResponse>>(conta)
            };

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Consultou uma listagem de contas.");
            logger.LogInformation("Consultou uma listagem de contas");

            return response;
        }

        public async Task<ContaResponse> Recuperar(int id)
        {
            Conta conta = await contasServico.Validar(id);

            var response = mapper.Map<ContaResponse>(conta);

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Recuperou a informação de um conta.");
            logger.LogInformation("Recuperou uma informação de conta");

            return response;
        }

        public async Task<ContaResponse> Inserir(ContaCadastroEdicaoRequest request)
        {
            Conta conta = await contasServico.Inserir(request.Nome);

            var response = mapper.Map<ContaResponse>(conta);

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Inseriu uma nova conta.");
            logger.LogInformation("Inseriu uma nova conta");

            return response;
        }

        public async Task<ContaResponse> Editar(int id, ContaCadastroEdicaoRequest request)
        {
            Conta conta = await contasServico.Atualizar(id, request.Nome);

            var response = mapper.Map<ContaResponse>(conta);

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Editou uma conta.");
            logger.LogInformation("Editou uma conta");

            return response;
        }

        public async Task<ContaResponse> Inativar(int id)
        {
            Conta conta = await contasServico.Inativar(id);

            var response = mapper.Map<ContaResponse>(conta);

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Inativou uma conta.");
            logger.LogInformation("Inativou uma conta");

            return response;
        }
    }
}
