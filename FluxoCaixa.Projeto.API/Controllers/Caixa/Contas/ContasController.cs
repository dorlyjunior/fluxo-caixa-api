using FluxoCaixa.Projeto.Aplicacao.Caixa.Contas.Servicos.Interfaces;
using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Requests;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace FluxoCaixa.Projeto.API.Controllers.Caixa.Contas
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "caixa")]
    public class ContasController : ControllerBase
    {
        private readonly IContasAppServico contasAppServico;
        private readonly IContaConsolidadoDiariosAppServico consolidadosAppServico;

        public ContasController(
            IContasAppServico contasAppServico,
            IContaConsolidadoDiariosAppServico consolidadosAppServico
        )
        {
            this.contasAppServico = contasAppServico;
            this.consolidadosAppServico = consolidadosAppServico;
        }

        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/contas")]
        public async Task<ActionResult<PaginacaoResponse<ContaResponse>>> Recuperar([FromQuery] ContaListagemRequest request)
        {
            PaginacaoResponse<ContaResponse> response = await contasAppServico.Recuperar(request);

            return Ok(response);
        }
        
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/contas/consolidados-diarios")]
        public async Task<ActionResult<PaginacaoResponse<ContaConsolidadoDiarioResponse>>> RecuperarConsolidados([FromQuery] ContaConsolidadoDiarioListagemRequest request)
        {
            PaginacaoResponse<ContaConsolidadoDiarioResponse> response = await consolidadosAppServico.Recuperar(request);

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/contas/consolidados-diarios/{id}/{dia}")]
        public async Task<ActionResult<ContaConsolidadoDiarioResponse>> RecuperarConsolidados(int id, string dia)
        {
            var diaDecoded = WebUtility.UrlDecode(dia);

            ContaConsolidadoDiarioResponse response = await consolidadosAppServico.Recuperar(id, diaDecoded);

            return Ok(response);
        }
        
        [HttpPut]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/contas/consolidados-diarios/{id}/{dia}/consolidacao")]
        public async Task<ActionResult<ContaConsolidadoDiarioResponse>> ConsolidarDia(int id, string dia)
        {
            var diaDecoded = WebUtility.UrlDecode(dia);

            ContaConsolidadoDiarioResponse response = await consolidadosAppServico.ConsolidarDia(id, diaDecoded);

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/contas/{id}")]
        public async Task<ActionResult<ContaResponse>> Recuperar(int id)
        {
            ContaResponse response = await contasAppServico.Recuperar(id);

            return Ok(response);
        }


        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/contas")]
        public async Task<ActionResult<ContaResponse>> Inserir([FromBody] ContaCadastroEdicaoRequest request)
        {
            ContaResponse response = await contasAppServico.Inserir(request);

            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/contas/{id}")]
        public async Task<ActionResult<ContaResponse>> Editar(int id, [FromBody] ContaCadastroEdicaoRequest request)
        {
            ContaResponse response = await contasAppServico.Editar(id, request);

            return Ok(response);
        }

        [HttpDelete]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/contas/{id}")]
        public async Task<ActionResult> Inativar(int id)
        {
            await contasAppServico.Inativar(id);

            return Ok();
        }
    }
}