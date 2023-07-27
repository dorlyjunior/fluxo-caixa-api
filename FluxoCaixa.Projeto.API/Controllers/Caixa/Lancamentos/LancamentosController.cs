using FluxoCaixa.Projeto.Aplicacao.Caixa.Lancamentos.Servicos.Interfaces;
using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Lancamentos.Requests;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Lancamentos.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.API.Controllers.Caixa.Lancamentos
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "caixa")]
    public class LancamentosController : ControllerBase
    {
        private readonly ILancamentosAppServico lancamentosAppServico;

        public LancamentosController(ILancamentosAppServico lancamentosAppServico)
        {
            this.lancamentosAppServico = lancamentosAppServico;
        }

        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/lancamentos")]
        public async Task<ActionResult<PaginacaoResponse<LancamentoResponse>>> Recuperar([FromQuery] LancamentoListagemRequest request)
        {
            PaginacaoResponse<LancamentoResponse> response = await lancamentosAppServico.Recuperar(request);

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/lancamentos/{id}")]
        public async Task<ActionResult<LancamentoResponse>> Recuperar(int id)
        {
            LancamentoResponse response = await lancamentosAppServico.Recuperar(id);

            return Ok(response);
        }


        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/lancamentos/creditos")]
        public async Task<ActionResult<LancamentoResponse>> InserirCredito([FromBody] LancamentoCadastroRequest request)
        {
            LancamentoResponse response = await lancamentosAppServico.LancarCredito(request);

            return Ok(response);
        }
        
        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/lancamentos/debitos")]
        public async Task<ActionResult<LancamentoResponse>> InserirDebito([FromBody] LancamentoCadastroRequest request)
        {
            LancamentoResponse response = await lancamentosAppServico.LancarDebito(request);

            return Ok(response);
        }

        [HttpDelete]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/lancamentos/creditos/{id}/{idTransacao}")]
        public async Task<ActionResult> ExcluirCredito(int id, string idTransacao)
        {
            LancamentoResponse response = await lancamentosAppServico.RemoverCredito(id, idTransacao);

            return Ok(response);
        }
        
        [HttpDelete]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/lancamentos/debitos/{id}/{idTransacao}")]
        public async Task<ActionResult> ExcluirDebito(int id, string idTransacao)
        {
            LancamentoResponse response = await lancamentosAppServico.RemoverDebito(id, idTransacao);

            return Ok(response);
        }
    }
}