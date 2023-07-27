using System;

namespace FluxoCaixa.Projeto.DataTransfer.Caixa.Auditorias.Responses
{
    public class AuditoriaResponse
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Acao { get; set; }
        public DateTime DataAcao { get; set; }
    }
}
