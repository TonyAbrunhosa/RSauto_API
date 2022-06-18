using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace RSauto.Domain.Entities
{
    [Table("LISTA_PRECO_PECAS")]
    public class ListaPrecoPecasEntity
    {
        [Key]
        public int ID_PRECO_PECA { get; set; }
        public int ID_MARCA_PECAS { get; set; }
        public int ID_MODELO { get; set; }
        public int ID_PECA { get; set; }        
        public bool STATUS { get; set; }
        public string CODIGO_PECA { get; set; }
        [Write(false)]
        public HistoricoPrecosPecasEntity HistoricoPrecosPecas { get; set; }
        [Write(false)]
        public List<ListaAnoModeloPrecoEntity> ListaAnoModeloPreco { get; set; }
        [Write(false)]
        public EstoquePecasEntity EstoquePecas { get; set; }
    }
}
