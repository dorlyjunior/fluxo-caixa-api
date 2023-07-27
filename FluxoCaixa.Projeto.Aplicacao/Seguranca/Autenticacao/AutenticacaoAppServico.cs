using FluxoCaixa.Projeto.Aplicacao.Seguranca.Autenticacao.Interfaces;
using FluxoCaixa.Projeto.Biblioteca.Especificacoes;
using FluxoCaixa.Projeto.DataTransfer.Seguranca.Autenticacao.Requests;
using FluxoCaixa.Projeto.DataTransfer.Seguranca.Autenticacao.Responses;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Servicos.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace FluxoCaixa.Projeto.Aplicacao.Seguranca.Autenticacao
{
    public class AutenticacaoAppServico : IAutenticacaoAppServico
    {
        private readonly IConfiguration configuracao;
        private readonly IAuditoriasServico auditoriasServico;

        public AutenticacaoAppServico(
            IConfiguration configuracao,
            IAuditoriasServico auditoriasServico
        )
        {
            this.configuracao = configuracao;
            this.auditoriasServico = auditoriasServico;
        }

        public async Task<AutenticacaoResponse> AutenticarUsuario(AutenticacaoRequest request)
        {
            var nomeUsuarioLogado = "Teste";

            string id = Convert.ToString(1);
            string email = request.Login;
            string nome = nomeUsuarioLogado;

            string token = TokenEspecificacao.ConstruirTokenUsuario(
                configuracao["JWT:Key"], 
                configuracao["JWT:Issuer"],
                id,
                email,
                nome
            );

            var response = new AutenticacaoResponse()
            {
                Token = token,
                Nome = nomeUsuarioLogado,
                Email = request.Login
            };

            await auditoriasServico.Inserir(response.Nome, "Logou no sistema.");

            return response;
        }
    }
}
