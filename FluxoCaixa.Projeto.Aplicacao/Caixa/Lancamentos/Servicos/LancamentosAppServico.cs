using AutoMapper;
using FluxoCaixa.Projeto.Aplicacao.Caixa.Lancamentos.Servicos.Interfaces;
using FluxoCaixa.Projeto.Biblioteca.Filtros;
using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using FluxoCaixa.Projeto.Biblioteca.Sessao;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Lancamentos.Requests;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Lancamentos.Responses;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Servicos;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Servicos.Interfaces;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Enumeradores;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Repositorios;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Aplicacao.Caixa.Lancamentos.Servicos
{
    public class LancamentosAppServico : ILancamentosAppServico
    {

        private readonly ILancamentoRepositorio lancamentoRepositorio;
        private readonly ILancamentosServico lancamentosServico;
        private readonly IMapper mapper;
        private readonly ISessao sessao;
        private readonly IAuditoriasServico auditoriasServico;

        public LancamentosAppServico(
            ILancamentoRepositorio lancamentoRepositorio,
            ILancamentosServico lancamentosServico,
            IMapper mapper,
            ISessao sessao,
            IAuditoriasServico auditoriasServico
        )
        {
            this.lancamentoRepositorio = lancamentoRepositorio;
            this.lancamentosServico = lancamentosServico;
            this.mapper = mapper;
            this.sessao = sessao;
            this.auditoriasServico = auditoriasServico;
        }

        public async Task<PaginacaoResponse<LancamentoResponse>> Recuperar(LancamentoListagemRequest request)
        {
            Expression<Func<Lancamento, bool>> filtros = PredicateBuilder.True<Lancamento>();

            request = request ?? new LancamentoListagemRequest();

            if (request.Id != 0)
            {
                filtros = filtros.And(p => p.Id == request.Id);
            }

            if (!string.IsNullOrWhiteSpace(request.IdTransacao))
            {
                filtros = filtros.And(p => p.IdTransacao == request.IdTransacao);
            }

            if (request.CodigoStatus != 0)
            {
                filtros = filtros.And(p => p.Status == (LancamentoStatusEnum)request.CodigoStatus);
            }
            
            if (request.CodigoTipo != 0)
            {
                filtros = filtros.And(p => p.Tipo == (LancamentoTipoEnum)request.CodigoTipo);
            }

            if (!string.IsNullOrWhiteSpace(request.Descricao))
            {
                filtros = filtros.And(p => p.Descricao.Contains(request.Descricao));
            }

            if (request.DataLancamentoDe != DateTime.MinValue)
            {
                filtros = filtros.And(p => p.DataLancamento >= request.DataLancamentoDe);
            }

            if (request.DataLancamentoAte != DateTime.MinValue)
            {
                filtros = filtros.And(p => p.DataLancamento <= request.DataLancamentoAte);
            }
            
            if (request.ValorDe > 0)
            {
                filtros = filtros.And(p => p.Valor >= request.ValorDe);
            }

            if (request.ValorAte > 0)
            {
                filtros = filtros.And(p => p.Valor <= request.ValorAte);
            }

            IList<Lancamento> lancamento = await lancamentoRepositorio.Recuperar(request.Pg, request.Ps, request.SortBy, request.Order, filtros);
            long total = await lancamentoRepositorio.RecuperarTotal(filtros);

            var response = new PaginacaoResponse<LancamentoResponse>()
            {
                PageIndex = request.Pg,
                PageSize = request.Ps,
                Total = total,
                List = mapper.Map<IList<LancamentoResponse>>(lancamento)
            };

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Pesquisou informações de lançamentos");

            return response;
        }

        public async Task<LancamentoResponse> Recuperar(int id)
        {
            Lancamento lancamento = await lancamentosServico.Validar(id);

            var response = mapper.Map<LancamentoResponse>(lancamento);

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Recuperou um detalhamento de um lançamento");

            return response;
        }
        public async Task<LancamentoResponse> LancarCredito(LancamentoCadastroRequest request)
        {
            Lancamento lancamento = await lancamentosServico.InserirCredito(request.IdConta, request.Descricao, request.Valor);

            var response = mapper.Map<LancamentoResponse>(lancamento);

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Lançou um crédito");

            return response;
        }
        
        public async Task<LancamentoResponse> LancarDebito(LancamentoCadastroRequest request)
        {
            Lancamento lancamento = await lancamentosServico.InserirDebito(request.IdConta, request.Descricao, request.Valor);

            var response = mapper.Map<LancamentoResponse>(lancamento);

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Lançou um débito");

            return response;
        }

        public async Task<LancamentoResponse> RemoverCredito(int id, string idTransacao)
        {
            Lancamento lancamentoDebito = await lancamentosServico.CancelarLancamentoCredito(id, idTransacao);

            var response = mapper.Map<LancamentoResponse>(lancamentoDebito);

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Cancelou um crédito");

            return response;
        }

        public async Task<LancamentoResponse> RemoverDebito(int id, string idTransacao)
        {
            Lancamento lancamentoCredito = await lancamentosServico.CancelarLancamentoDebito(id, idTransacao);

            var response = mapper.Map<LancamentoResponse>(lancamentoCredito);

            await auditoriasServico.Inserir(sessao.GetNomeDoUsuario(), "Cancelou um débito");

            return response;
        }
    }
}
