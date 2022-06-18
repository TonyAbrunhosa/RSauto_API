using System.Collections.Generic;

namespace RSauto.Domain.Entities.Cadastro.ListaPrecoPecas.Output
{
    public class ListarListaPrecoPecasOutput
    {
        public int ID_PRECO_PECA { get; set; }
        public string DESCRICAO_PECA { get; set; }
        public string DESCRICAO_MARCA_PECA { get; set; }
        public string MODELO_VEICULO { get; set; }
        public int QTDE_ESTOQUE { get; set; }
        public decimal CUSTO { get; set; }
        public decimal PRECO { get; set; }
        public IEnumerable<ListaAnoModeloPrecoOutput> ListaAnoModeloPreco { get; set; }
    }
}
