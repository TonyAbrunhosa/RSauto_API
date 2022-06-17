using Dapper.Contrib.Extensions;

namespace RSauto.Domain.Entities
{
    [Table("MARCAS_PECAS")]
    public class MarcasPecasEntity
    {
        [Key]
        public int ID_MARCA_PECAS { get; set; }
        public string DESCRICAO { get; set; }
    }
}
