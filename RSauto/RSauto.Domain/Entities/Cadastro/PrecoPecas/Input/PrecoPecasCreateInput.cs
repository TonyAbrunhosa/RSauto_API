using System.Collections.Generic;

namespace RSauto.Domain.Entities.Cadastro.PrecoPecas.Input
{
    public class PrecoPecasCreateInput
    {
        public int ID_MARCA_PECAS { get; set; }
        public int ID_MODELO { get; set; }
        public int ID_PECA { get; set; }
        public bool STATUS { get; set; }
        public string CODIGO_PECA { get; set; }
        public HistoricosPrecoPecasCreateInput HistoricoPrecosPecas { get; set; }
        public IEnumerable<ListaAnoModeloPrecoCreateinput> ListaAnoModeloPreco { get; set; }

        public class HistoricosPrecoPecasCreateInput
        {
            public int ID_FORNECEDOR { get; set; }            
            public decimal PRECO { get; set; }
            public decimal CUSTO { get; set; }
            public EstoquePecasCreateInput EstoquePecas { get; set; }
        }

        public class EstoquePecasCreateInput
        {
            public int QTDE_ESTOQUE { get; set; }            
            public string LOTE { get; set; }
        }

        public class ListaAnoModeloPrecoCreateinput
        {   
            public int ID_ANO_MOD_VEIC { get; set; }            
        } 
    }       
}
