namespace FluxoCaixa.Projeto.Biblioteca.Paginacao
{
    public class PaginacaoRequest
    {
        public int Pg { get; set; } = 1;
        public int Ps { get; set; } = 10;
        public string SortBy { get; set; } = "Id";
        public string Order { get; set; } = "Asc";

        public void SetPg(int pagina)
        {
            if (pagina != 0)
            {
                Pg = Pg - 1;
            }
            else
            {
                Pg = 0;
            }
        }
    }
}
