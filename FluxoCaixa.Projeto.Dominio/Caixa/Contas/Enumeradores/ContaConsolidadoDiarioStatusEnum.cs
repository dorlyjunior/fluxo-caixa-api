using System.ComponentModel;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Contas.Enumeradores
{
    public enum ContaConsolidadoDiarioStatusEnum
    {
        Consolidado = 1,
        [Description("Em Aberto")]
        EmAberto
    }
}
