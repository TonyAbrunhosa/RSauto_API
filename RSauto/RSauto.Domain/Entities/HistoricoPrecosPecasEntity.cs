using Dapper.Contrib.Extensions;
using System;

namespace RSauto.Domain.Entities
{
    [Table("HISTORICO_PRECOS_PECAS")]
    public class HistoricoPrecosPecasEntity
    {
        [Key]
        public int ID_HIST_PRECO { get; set; }
        public decimal PRECO { get; set; }
        public decimal CUSTO { get; set; }
        public DateTime ULTIMO_PRECO { get; set; }
        public int ID_PRECO_PECA { get; set; }
    }
}
