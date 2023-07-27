using FluentNHibernate.Mapping;
using FluxoCaixa.Projeto.Biblioteca.Enumeradores;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;

namespace FluxoCaixa.Projeto.Infraestrutura.Caixa.Contas.Mapeamentos
{
    public class ContaMap : ClassMap<Conta>
    {
        public ContaMap()
        {
            Table("conta");
            Id(p => p.Id).Column("id").GeneratedBy.Sequence("seq_conta");
            Map(p => p.Nome).Column("nome");
            Map(p => p.Saldo).Column("saldo");            
            Map(p => p.Status).Column("status").CustomType<AtivoInativoEnum>();            
        }
    }
}
