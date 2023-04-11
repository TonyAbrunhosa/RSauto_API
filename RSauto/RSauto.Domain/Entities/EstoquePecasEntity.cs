using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSauto.Domain.Entities
{
    [Table("ESTOQUE_PECAS")]
    public class EstoquePecasEntity
    {
        [Key]
        public int ID_ESTOQUE_PECAS { get; set; }
        public int QTDE_ESTOQUE { get; set; }
        public int ID_PRECO_PECA { get; set; }
        public string LOTE { get; set; }
    }
}
