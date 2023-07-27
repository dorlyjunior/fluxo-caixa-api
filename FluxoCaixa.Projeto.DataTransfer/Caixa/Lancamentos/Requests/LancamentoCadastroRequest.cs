namespace FluxoCaixa.Projeto.DataTransfer.Caixa.Lancamentos.Requests
{
    public class LancamentoCadastroRequest
    {
        public int IdConta { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}
