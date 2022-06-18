using Dapper.Contrib.Extensions;

namespace RSauto.Domain.Entities
{
    [Table("PECAS")]
    public class PecasEntity
    {
        [Key]
        public int ID_PECA { get; set; }
        public string DESCRICAO { get; set; }        
    }
}
