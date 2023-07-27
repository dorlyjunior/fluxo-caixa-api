using FluentNHibernate.Mapping;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Enumeradores;

namespace FluxoCaixa.Projeto.Infraestrutura.Caixa.Lancamentos.Mapeamentos
{
    public class LancamentoMap : ClassMap<Lancamento>
    {
        public LancamentoMap()
        {
            Table("lancamento");
            Id(p => p.Id).Column("id").GeneratedBy.Sequence("seq_lancamento");
            Map(p => p.IdTransacao).Column("id_transacao");
            Map(p => p.Descricao).Column("descricao");
            Map(p => p.Valor).Column("valor");
            Map(p => p.DataLancamento).Column("data_lancamento");
            Map(p => p.Tipo).Column("tipo").CustomType<LancamentoTipoEnum>();
            Map(p => p.Status).Column("status").CustomType<LancamentoStatusEnum>();
            References(p => p.Conta).Column("id_conta");
        }
    }
}
