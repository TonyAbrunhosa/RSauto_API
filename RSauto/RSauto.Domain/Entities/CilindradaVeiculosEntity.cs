using Dapper.Contrib.Extensions;

namespace RSauto.Domain.Entities
{
    [Table("CILINDRADA_VEICULOS")]
    public class CilindradaVeiculosEntity
    {
        [Key]
        public int ID_CILINDRADA { get; set; }
        public string DESCRICAO { get; set; }        
    }
}
