using FluentNHibernate.Mapping;
using FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Entidades;

namespace FluxoCaixa.Projeto.Infraestrutura.Caixa.Auditorias.Mapeamentos
{
    public class AuditoriaMap : ClassMap<Auditoria>
    {
        public AuditoriaMap()
        {
            Table("auditoria");
            Id(p => p.Id).Column("id").GeneratedBy.Sequence("seq_auditoria");
            Map(p => p.Usuario).Column("usuario");
            Map(p => p.Acao).Column("acao");
            Map(p => p.DataAcao).Column("data_acao");
        }
    }
}
