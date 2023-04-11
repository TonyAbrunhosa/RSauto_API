using System.Collections.Generic;

namespace RSauto.Domain.Entities.Cadastro.ListaPrecoPecas.Output
{
    public class ListaPrecoPecasOutput
    {
        public int ID_PRECO_PECA { get; set; }
        public int ID_MARCA_PECAS { get; set; }
        public int ID_MARCA { get; set; }
        public int ID_MODELO { get; set; }
        public int ID_PECA { get; set; }
        public bool STATUS { get; set; }
        public string CODIGO_PECA { get; set; }
        public int QTDE_ESTOQUE { get; set; }
        public decimal CUSTO { get; set; }
        public decimal PRECO { get; set; }
        public IEnumerable<ListaAnoModeloPrecoEntity> ListaAnoModeloPreco { get; set; }
    }
}
