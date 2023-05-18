using System.Collections.Generic;

namespace RSauto.Domain.Entities.Cadastro.PrecoPecas.Input
{
    public class PrecoPecaInput
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int IdMarca { get; set; }
        public string Marca { get; set; }
        public bool Status { get; set; }
        public string Codigo { get; set; }
        public VeiculoInput Veiculo { get; set; }        
        public IEnumerable<FornecedoresInput> Fornecedores { get; set; }
        public IEnumerable<AnoModelosInput> AnoModelos { get; set; }

        public class VeiculoInput
        {
            public MarcaInput Marca { get; set; }
            public IEnumerable<ModelosInput> Modelos { get; set; }

            public class MarcaInput
            {
                public int Id { get; set; }
                public string Descricao { get; set; }
            }
        }        

        public class ModelosInput
        {
            public int IdModVeicPeca { get; set; }
            public int Id { get; set; }
            public string Descricao { get; set; }
            public bool Remover { get; set; }
        }

        public class FornecedoresInput
        {
            public int IdHistPrecoPeca { get; set; }
            public int Id { get; set; }            
            public decimal Preco { get; set; }
            public decimal Custo { get; set; }
            public bool Status { get; set; }
            public int Estoque { get; set; }
            public string Lote { get; set; }
        }

        public class AnoModelosInput
        {
            public int IdAnoModPreco { get; set; }
            public int Id { get; set; }
            public string Descricao { get; set; }
            public bool Remover { get; set; }
        }
    }
}
