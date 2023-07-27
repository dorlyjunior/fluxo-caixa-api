using FluentNHibernate.Cfg;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using FluxoCaixa.Projeto.Infraestrutura.Caixa.Contas.Mapeamentos;
using NHibernate;

namespace FluxoCaixa.Projeto.Ioc.NHibernate
{
    public class SessionFactoryProvider
    {
        public SessionFactoryProvider() { }

        public static ISessionFactory CreateInstance(string connectionString, string schema)
        {
            return Fluently.Configure()
                .Database(() => {
                    return FluentNHibernate.Cfg.Db.PostgreSQLConfiguration.Standard
                    .ShowSql()
                    .FormatSql()
                    .DefaultSchema(schema)
                    .ConnectionString(connectionString);
                })
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Conta>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ContaMap>())
                .CurrentSessionContext("call")
                .BuildSessionFactory();
        }
    }
}
