using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace RSauto.Domain.Entities
{
    [Table("MARCAS_VEICULOS")]
    public class MarcasVeiculosEntity
    {
        [Key]
        public int ID_MARCA { get; set; }
        public string DESCRICAO { get; set; }

        [Write(false)]
        public IEnumerable<ModelosVeiculosEntity> MODELOS { get; set; }
    }
}
