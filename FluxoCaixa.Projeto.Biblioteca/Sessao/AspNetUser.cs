using Microsoft.AspNetCore.Http;
using System.Text;

namespace FluxoCaixa.Projeto.Biblioteca.Sessao
{
    public class AspNetUser : ISessao
    {
        private readonly IHttpContextAccessor acessor;

        public AspNetUser(IHttpContextAccessor acessor)
        {
            this.acessor = acessor;
        }

        public string Id => acessor.HttpContext.User.Identity.Name;

        public string Token => acessor.HttpContext.Request.Headers["Authorization"];

        public bool IsAuthenticated()
        {
            return acessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public int GetIdUsuario()
        {
            return int.Parse(acessor.HttpContext.User.FindFirst("Id").Value);
        }
        
        public string GetInfo(string name)
        {
            return acessor.HttpContext.User.FindFirst(name).Value;
        }

        public string GetNomeDoUsuario()
        {
            StringBuilder builder = new StringBuilder();

            string idUsuario = acessor.HttpContext.User.FindFirst("Id").Value;
            string nomeUsuario = acessor.HttpContext.User.FindFirst("Nome").Value;

            builder.AppendFormat("Id: {0} - Nome: {1} ", idUsuario, nomeUsuario);

            return builder.ToString();
        }
    }
}
