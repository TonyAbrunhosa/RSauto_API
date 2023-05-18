namespace RSauto.Domain.Entities.Cadastro.Cliente.Input
{
    public class ClienteListarInput: PageQuery
    {
        public string Nome { get; set; } = string.Empty;
        public string RazaoSocial { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
    }
}
