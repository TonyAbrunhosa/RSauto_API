using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace RSauto.Domain.Entities
{
    [Table("PRECO_PECAS")]
    public class PrecoPecasEntity
    {
        [Key]
        public int ID_PRECO_PECA { get; set; }
        public int ID_MARCA_PECAS { get; set; }
        public int ID_MODELO { get; set; }
        public int ID_PECA { get; set; }        
        public bool STATUS { get; set; }
        public string CODIGO_PECA { get; set; }
        [Write(false)]
        public HistoricosPrecoPecasEntity HistoricoPrecosPecas { get; set; }
        [Write(false)]
        public List<ListaAnoModeloPrecoEntity> ListaAnoModeloPreco { get; set; }        
    }
}
