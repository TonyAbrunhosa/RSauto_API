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
        public List<HistoricosPrecoPecasEntity> HistoricoPrecosPecas { get; set; }
        [Write(false)]
        public List<ListaAnoModeloPrecoEntity> ListaAnoModeloPreco { get; set; }
        [Write(false)]
        public PecasEntity Pecas { get; set; }
        [Write(false)]
        public MarcasVeiculosEntity MarcasVeiculos { get; set; }
        [Write(false)]
        public ModelosVeiculosEntity ModelosVeiculos { get; set; }
        [Write(false)]
        public MarcasPecasEntity MarcasPecas { get; set; }
    }
}
