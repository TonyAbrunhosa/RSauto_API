namespace RSauto.Domain.Entities.Cadastro.Fornecedor.Input
{
    public class FornecedorListarInput : PageQuery
    {
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string Documento { get; set; }
    }
}
