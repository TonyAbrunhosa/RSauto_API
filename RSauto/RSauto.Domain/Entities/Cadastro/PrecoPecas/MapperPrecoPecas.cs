using RSauto.Domain.Entities.Cadastro.PrecoPecas.Input;
using System;
using System.Collections.Generic;

namespace RSauto.Domain.Entities.Cadastro.PrecoPecas
{
    public static class MapperPrecoPecas
    {
        public static PrecoPecasEntity PrecoPecasEntity(this PrecoPecaInput input, int id = 0)
        {
            return new PrecoPecasEntity
            {
                ID_PRECO_PECA = id,
                ID_MARCA_PECAS = input.IdMarca,
                ID_PECA = input.Id,
                STATUS = true,
                CODIGO_PECA = input.Codigo,
                HistoricoPrecosPecas = SetHistoricoPrecosPecas(input.Fornecedores),
                ListaAnoModeloPreco = SetListAnoModeloPreco(input.AnoModelos),
                Pecas = new PecasEntity { ID_PECA = input.Id, DESCRICAO = input.Descricao},
                MarcasPecas = new MarcasPecasEntity { ID_MARCA_PECAS = input.IdMarca, DESCRICAO = input.Marca },
                MarcasVeiculos = new MarcasVeiculosEntity { ID_MARCA = input.Veiculo.Marca.Id, DESCRICAO = input.Veiculo.Marca.Descricao },
                ModelosVeiculos = SetModelosVeiculos(input.Veiculo.Modelos)
            };
        }          

        private static List<ListaAnoModeloPrecoEntity> SetListAnoModeloPreco(IEnumerable<PrecoPecaInput.AnoModelosInput> anoModelosinput)
        {
            List<ListaAnoModeloPrecoEntity> entities = new List<ListaAnoModeloPrecoEntity>();

            foreach (var item in anoModelosinput)
                entities.Add(new ListaAnoModeloPrecoEntity { 
                    ID_ANO_MOD_VEIC = item.Id, 
                    REMOVER = item.Remover, 
                    ID_ANO_MOD_PRECO = item.IdAnoModPreco,
                    AnoModeloVeiculo = new AnoModeloVeiculoEntity { DESCRICAO = item.Descricao, ID_ANO_MOD_VEIC = item.Id}
                });

            return entities;
        }

        private static List<HistoricosPrecoPecasEntity> SetHistoricoPrecosPecas(IEnumerable<PrecoPecaInput.FornecedoresInput> fornInput)
        {
            List<HistoricosPrecoPecasEntity> entites = new List<HistoricosPrecoPecasEntity>();

            foreach (var item in fornInput)
                entites.Add(new HistoricosPrecoPecasEntity
                {
                    ID_HIST_PRECO_PECA = item.IdHistPrecoPeca,
                    ID_FORNECEDOR = item.Id,
                    PRECO = item.Preco,
                    CUSTO = item.Custo,
                    DATA_PRECO = DateTime.Now,
                    QTDE_ESTOQUE = item.Estoque,
                    LOTE = item.Lote                    
                });

            return entites;
        }

        private static List<ModelosVeiculosPecasEntity> SetModelosVeiculos(IEnumerable<PrecoPecaInput.ModelosInput> modelosveiculosInput)
        {
            List<ModelosVeiculosPecasEntity> entities = new List<ModelosVeiculosPecasEntity>();

            foreach (var item in modelosveiculosInput)
            {
                entities.Add(new ModelosVeiculosPecasEntity
                {
                    ID_MOD_VEIC_PECAS = item.IdModVeicPeca,
                    ID_MODELO = item.Id,
                    modelosVeiculos = new ModelosVeiculosEntity { ID_MODELO = item.Id, DESCRICAO = item.Descricao }
                });
            }

            return entities;
        }       
    }
}
