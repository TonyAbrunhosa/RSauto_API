using Dapper.Contrib.Extensions;

namespace RSauto.Domain.Entities
{
    [Table("ANO_MODELO_VEICULO")]
    public class AnoModeloVeiculoEntity
    {
        [Key]
        public int ID_ANO_MOD_VEIC { get; set; }
        public string DESCRICAO { get; set; }
    }
}
