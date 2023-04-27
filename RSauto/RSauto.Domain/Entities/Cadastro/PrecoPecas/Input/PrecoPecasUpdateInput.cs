using System.Collections.Generic;

namespace RSauto.Domain.Entities.Cadastro.PrecoPecas.Input
{
    public  class PrecoPecasUpdateInput
    {
        public int ID_MARCA_PECAS { get; set; }
        public string DESC_MARCA_PECAS { get; set; }
        public int ID_MARCA { get; set; }
        public string DESC_MARCA { get; set; }
        public int ID_MODELO { get; set; }
        public string DESC_MODELO { get; set; }
        public int ID_PECA { get; set; }
        public string DESC_PECA { get; set; }
        public bool STATUS { get; set; }
        public string CODIGO_PECA { get; set; }
        public HistoricosPrecoPecasUpdateInput HistoricoPrecosPecas { get; set; }
        public IEnumerable<ListaAnoModeloPrecoUpdateInput> ListaAnoModeloPreco { get; set; }

        public class HistoricosPrecoPecasUpdateInput
        {
            public int ID_HIST_PRECO_PECA { get; set; }
            public int ID_FORNECEDOR { get; set; }
            public string DESC_FORNECEDOR { get; set; }
            public bool STATUS { get; set; }
            public EstoquePecasUpdateInput EstoquePecas { get; set; }
        }

        public class EstoquePecasUpdateInput
        {
            public int ID_ESTOQUE_PECAS { get; set; }
            public int QTDE_ESTOQUE { get; set; }            
            public string LOTE { get; set; }
        }

        public class ListaAnoModeloPrecoUpdateInput
        {
            public int ID_ANO_MOD_PRECO { get; set; }
            public int ID_PRECO_PECA { get; set; }
            public int ID_ANO_MOD_VEIC { get; set; }
            public bool REMOVER { get; set; }
        }
    }    
}
