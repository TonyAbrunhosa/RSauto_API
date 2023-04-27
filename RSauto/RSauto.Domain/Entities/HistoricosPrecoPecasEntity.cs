using Dapper.Contrib.Extensions;
using System;

namespace RSauto.Domain.Entities
{
    [Table("HISTORICOS_PRECO_PECAS")]
    public class HistoricosPrecoPecasEntity
    {
        [Key]
        public int ID_HIST_PRECO_PECA { get; set; }
        public int ID_FORNECEDOR { get; set; }
        public int ID_PRECO_PECA { get; set; }
        public int ID_ESTOQUE_PECAS { get; set; }
        public decimal PRECO { get; set; }
        public decimal CUSTO { get; set; }
        public DateTime DATA_PRECO { get; set; }
        public bool STATUS { get; set; }
        [Write(false)]
        public EstoquePecasEntity EstoquePecas { get; set; }
        [Write(false)]
        public FornecedoresEntity Fornecedores { get; set; }
    }
}
