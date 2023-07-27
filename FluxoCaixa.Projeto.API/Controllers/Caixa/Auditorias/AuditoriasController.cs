using FluxoCaixa.Projeto.Aplicacao.Caixa.Auditorias.Servicos.Interfaces;
using FluxoCaixa.Projeto.Biblioteca.Paginacao;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Auditorias.Requests;
using FluxoCaixa.Projeto.DataTransfer.Caixa.Auditorias.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.API.Controllers.Caixa.Auditorias
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "caixa")]
    public class AuditoriasController : ControllerBase
    {
        private readonly IAuditoriasAppServico auditoriasAppServico;
        private readonly ILogger<AuditoriasController> logger;

        public AuditoriasController(
            IAuditoriasAppServico auditoriasAppServico,
            ILogger<AuditoriasController> logger
        )
        {
            this.auditoriasAppServico = auditoriasAppServico;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("api/caixa/auditorias")]
        public async Task<ActionResult<PaginacaoResponse<AuditoriaResponse>>> Recuperar([FromQuery] AuditoriaListagemRequest request)
        {
            PaginacaoResponse<AuditoriaResponse> response = await auditoriasAppServico.Recuperar(request);

            return Ok(response);
        }
    }
}
