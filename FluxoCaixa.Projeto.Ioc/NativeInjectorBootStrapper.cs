
using FluxoCaixa.Projeto.Aplicacao.Caixa.Contas.Servicos;
using FluxoCaixa.Projeto.Biblioteca.Sessao;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Servicos;
using FluxoCaixa.Projeto.Infraestrutura.Caixa.Contas.Repositorios;
using FluxoCaixa.Projeto.Ioc.NHibernate;
using FluxoCaixa.Projeto.Ioc.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using ISession = NHibernate.ISession;

namespace FluxoCaixa.Projeto.Ioc
{
    public class NativeInjectorBootStrapper
    {
        public static void Registrar(IServiceCollection services, IConfiguration configuracao)
        {
            string conexao = configuracao.GetConnectionString("DefaultConnection");
            string schema = configuracao.GetSection("Schema").Value;

            // NHibernate
            services.AddSingleton<ISessionFactory>(factory =>
            {
                return SessionFactoryProvider.CreateInstance(conexao, schema);
            });

            // ISession
            services.AddScoped<ISession>(factory => {
                return factory.GetService<ISessionFactory>().OpenSession();
            });

            // UnitOfWork
            services.AddScoped<IUnitOfWorkAsync, UnitOfWorkAsync>();

            // Sessao
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISessao, AspNetUser>();

            // Services
            services.Scan(scan => scan
                .FromAssemblyOf<ContasAppServico>()
                    .AddClasses()
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<ContaRepositorioAsync>()
                    .AddClasses()
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<ContasServico>()
                    .AddClasses()
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());
        }
    }
}
