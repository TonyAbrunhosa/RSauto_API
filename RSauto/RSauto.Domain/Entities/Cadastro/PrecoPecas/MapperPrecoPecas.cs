using RSauto.Domain.Entities.Cadastro.PrecoPecas.Input;
using System;
using System.Collections.Generic;

namespace RSauto.Domain.Entities.Cadastro.PrecoPecas
{
    public static class MapperPrecoPecas
    {
        public static PrecoPecasEntity PrecoPecasCreateEntity(this PrecoPecasCreateInput input)
        {
            return new PrecoPecasEntity
            {
                ID_MARCA_PECAS = input.ID_MARCA_PECAS,
                ID_MODELO = input.ID_MODELO,
                ID_PECA = input.ID_PECA,
                STATUS = true,
                CODIGO_PECA = input.CODIGO_PECA,
                HistoricoPrecosPecas = new HistoricosPrecoPecasEntity
                {
                    ID_FORNECEDOR = input.HistoricoPrecosPecas.ID_FORNECEDOR,
                    PRECO = input.HistoricoPrecosPecas.PRECO,
                    CUSTO = input.HistoricoPrecosPecas.CUSTO,
                    DATA_PRECO = DateTime.Now,
                    EstoquePecas = new EstoquePecasEntity
                    {
                        QTDE_ESTOQUE = input.HistoricoPrecosPecas.EstoquePecas.QTDE_ESTOQUE,
                        LOTE = input.HistoricoPrecosPecas.EstoquePecas.LOTE,
                    }
                },
                ListaAnoModeloPreco = SetListAnoModeloPrecoCreate(input.ListaAnoModeloPreco)
            };
        }

        public static PrecoPecasEntity PrecoPecasUpdateEntity(this PrecoPecasUpdateInput input, int id)
        {
            return new PrecoPecasEntity
            {
                ID_PRECO_PECA= id,
                ID_MARCA_PECAS = input.ID_MARCA_PECAS,
                ID_MODELO = input.ID_MODELO,
                ID_PECA = input.ID_PECA,
                STATUS = input.STATUS,
                CODIGO_PECA = input.CODIGO_PECA,
                HistoricoPrecosPecas = new HistoricosPrecoPecasEntity
                {
                    ID_FORNECEDOR = input.HistoricoPrecosPecas.ID_FORNECEDOR,
                    ID_HIST_PRECO_PECA = input.HistoricoPrecosPecas.ID_HIST_PRECO_PECA,
                    STATUS = input.HistoricoPrecosPecas.STATUS,
                    EstoquePecas = new EstoquePecasEntity
                    {
                        ID_ESTOQUE_PECAS= input.HistoricoPrecosPecas.ID_HIST_PRECO_PECA,
                        QTDE_ESTOQUE = input.HistoricoPrecosPecas.EstoquePecas.QTDE_ESTOQUE,
                        LOTE = input.HistoricoPrecosPecas.EstoquePecas.LOTE,
                    }
                },
                ListaAnoModeloPreco = SetListAnoModeloPrecoUpdate(input.ListaAnoModeloPreco)
            };
        }

        private static List<ListaAnoModeloPrecoEntity> SetListAnoModeloPrecoCreate(IEnumerable<PrecoPecasCreateInput.ListaAnoModeloPrecoCreateinput> listaAnoModeloPreco)
        {
            List<ListaAnoModeloPrecoEntity> entities = new List<ListaAnoModeloPrecoEntity>();

            foreach (var item in listaAnoModeloPreco)
                entities.Add(new ListaAnoModeloPrecoEntity { ID_ANO_MOD_VEIC = item.ID_ANO_MOD_VEIC });

            return entities;
        }

        private static List<ListaAnoModeloPrecoEntity> SetListAnoModeloPrecoUpdate(IEnumerable<PrecoPecasUpdateInput.ListaAnoModeloPrecoUpdateInput> listaAnoModeloPreco)
        {
            List<ListaAnoModeloPrecoEntity> entities = new List<ListaAnoModeloPrecoEntity>();

            foreach (var item in listaAnoModeloPreco)
                entities.Add(new ListaAnoModeloPrecoEntity { ID_ANO_MOD_VEIC = item.ID_ANO_MOD_VEIC, REMOVER = item.REMOVER, ID_ANO_MOD_PRECO = item.ID_ANO_MOD_PRECO });

            return entities;
        }
    }
}
