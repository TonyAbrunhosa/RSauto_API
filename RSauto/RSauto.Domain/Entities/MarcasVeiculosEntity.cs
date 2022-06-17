using Dapper.Contrib.Extensions;

namespace RSauto.Domain.Entities
{
    [Table("MARCAS_VEICULOS")]
    public class MarcasVeiculosEntity
    {
        [Key]
        public int ID_MARCA { get; set; }
        public string NOME { get; set; }
    }
}
