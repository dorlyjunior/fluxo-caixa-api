namespace FluxoCaixa.Projeto.DataTransfer.Caixa.Contas.Responses
{
    public class ContaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Saldo { get; set; }
        public string Status { get; set; }
        public int CodigoStatus { get; set; }
    }
}
