using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluxoCaixa.Projeto.Biblioteca.Excecoes;

namespace FluxoCaixa.Projeto.Biblioteca.Especificacoes
{
    public class TokenEspecificacao
    {
        public static string ConstruirTokenUsuario(
            string key, 
            string issuer, 
            string id, 
            string email, 
            string nome
        )
        {
            Claim[] claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("Id", id),
                new Claim("Nome", nome)
            };

            SymmetricSecurityKey chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            SigningCredentials credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static JwtSecurityToken IsValido(string token, string issuer, string key)
        {
            var handler = new JwtSecurityTokenHandler();

            var options = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };

            try
            {
                handler.ValidateToken(token, options, out var validatedToken);

                return (JwtSecurityToken)validatedToken;
            }
            catch (SecurityTokenException e)
            {
                throw new RegraDeNegocioExcecao("Este link está expirado. Solicite novamente a troca de senha.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
