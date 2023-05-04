using Dapper.Contrib.Extensions;

namespace RSauto.Domain.Entities
{
    [Table("LISTA_ANO_MODELO_PRECO")]
    public class ListaAnoModeloPrecoEntity
    {
        [Key]
        public int ID_ANO_MOD_PRECO { get; set; }
        public int ID_PRECO_PECA { get; set; }
        public int ID_ANO_MOD_VEIC { get; set; }
        [Write(false)]
        public bool REMOVER { get; set; }
        [Write(false)]
        public AnoModeloVeiculoEntity AnoModeloVeiculo { get; set; }
    }
}
