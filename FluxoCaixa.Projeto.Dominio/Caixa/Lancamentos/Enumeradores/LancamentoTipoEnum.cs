using System.ComponentModel;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Enumeradores
{
    public enum LancamentoTipoEnum
    {
        [Description("Crédito")]
        Credito = 1,
        [Description("Débito")]
        Debito
    }
}
