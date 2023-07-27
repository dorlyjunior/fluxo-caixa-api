using System.ComponentModel;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Enumeradores
{
    public enum LancamentoStatusEnum
    {
        Efetivado = 1,
        [Description("Em Análise")]
        EmAnalise,
        Cancelado,
        Estornado
    }
}
