using Dapper.Contrib.Extensions;

namespace RSauto.Domain.Entities
{
    [Table("FORNECEDORES")]
    public class FornecedoresEntity
    {
        [Key]
        public int ID_FORNECEDOR { get; set; }
        public string DESCRICAO { get; set; }
    }
}
