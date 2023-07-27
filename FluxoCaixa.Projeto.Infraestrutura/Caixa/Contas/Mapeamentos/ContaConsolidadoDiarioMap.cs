using FluentNHibernate.Mapping;
using FluxoCaixa.Projeto.Biblioteca.Enumeradores;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Enumeradores;

namespace FluxoCaixa.Projeto.Infraestrutura.Caixa.Contas.Mapeamentos
{
    public class ContaConsolidadoDiarioMap : ClassMap<ContaConsolidadoDiario>
    {
        public ContaConsolidadoDiarioMap()
        {
            Table("consolidado_diario");
            Id(p => p.Id).Column("id").GeneratedBy.Sequence("seq_consolidado_diario");
            Map(p => p.DataDia).Column("data_dia");
            Map(p => p.Dia).Column("dia");
            Map(p => p.Mes).Column("mes");
            Map(p => p.Ano).Column("ano");
            Map(p => p.DiaSemana).Column("dia_semana");
            Map(p => p.TotalCredito).Column("total_credito");
            Map(p => p.TotalDebito).Column("total_debito");
            Map(p => p.DataConsolidacao).Column("data_consolidacao");
            Map(p => p.Saldo).Column("saldo");
            Map(p => p.Status).Column("status").CustomType<ContaConsolidadoDiarioStatusEnum>();
            References(p => p.Conta).Column("id_conta");
        }
    }
}
