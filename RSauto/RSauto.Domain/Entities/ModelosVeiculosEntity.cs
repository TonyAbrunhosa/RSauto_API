using Dapper.Contrib.Extensions;

namespace RSauto.Domain.Entities
{
    [Table("MODELOS_VEICULOS")]
    public class ModelosVeiculosEntity
    {
        [Key]
        public int ID_MODELO { get; set; }
        public string DESCRICAO { get; set; }
        public int ID_MARCA { get; set; }
    }
}
