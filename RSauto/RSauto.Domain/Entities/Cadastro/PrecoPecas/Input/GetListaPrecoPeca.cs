namespace RSauto.Domain.Entities.Cadastro.PrecoPecas.Input
{
    public class GetListaPrecoPeca
    {
        public string? Filtro { get; set; } = "";
        public int Pagina { get; set; } = 1;
        public int QtdPorPagina { get; set; } = 15;
    }
}
