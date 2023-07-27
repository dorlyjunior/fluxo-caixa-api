using FluxoCaixa.Projeto.Biblioteca.Excecoes;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.API.Middlewares
{
    public class TratamentoDeErrosMiddleware
    {
        private readonly RequestDelegate next;

        public TratamentoDeErrosMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            string mensagem = exception.Message;

            Log.Error(exception, "Error");

            if (exception is UnauthorizedAccessException)
            {
                code = HttpStatusCode.Unauthorized;
                mensagem = "Usuário não está autenticado.";
            }

            if (exception is NaoAutorizadoExcecao)
            {
                code = HttpStatusCode.Forbidden;
                mensagem = "Acesso negado. " + exception.Message;
            }

            if (exception is RegraDeNegocioExcecao)
            {
                code = HttpStatusCode.BadRequest;
                mensagem = exception.Message;
            }

            string response = JsonSerializer.Serialize(new { message = mensagem });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(response);
        }
    }
}
