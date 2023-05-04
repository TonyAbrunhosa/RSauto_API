using Dapper.Contrib.Extensions;

namespace RSauto.Domain.Entities
{
    [Table("MODELOS_VEICULOS_PECAS")]
    public class ModelosVeiculosPecasEntity
    {
        [Key]
        public int ID_MOD_VEIC_PECAS { get; set; }
        public int ID_PRECO_PECA { get; set; }
        public int ID_MODELO { get; set; }
        [Write(false)]
        public ModelosVeiculosEntity modelosVeiculos { get; set; }
        [Write(false)]
        public bool REMOVER { get; set; }
    }
}
