using FluxoCaixa.Projeto.Aplicacao.Seguranca.Autenticacao.Interfaces;
using FluxoCaixa.Projeto.DataTransfer.Seguranca.Autenticacao.Requests;
using FluxoCaixa.Projeto.DataTransfer.Seguranca.Autenticacao.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.API.Controllers.Seguranca.Autenticacao
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "seguranca")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoAppServico autenticacaoAppServico;

        public AutenticacaoController(IAutenticacaoAppServico autenticacaoAppServico)
        {
            this.autenticacaoAppServico = autenticacaoAppServico;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/autenticacao")]
        public async Task<ActionResult<AutenticacaoResponse>> AutenticarUsuario([FromBody] AutenticacaoRequest request)
        {
            AutenticacaoResponse response = await autenticacaoAppServico.AutenticarUsuario(request);

            return Ok(response);
        }
    }
}