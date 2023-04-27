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
                HistoricoPrecosPecas = SetHistoricoPrecosPecasCreate(input.HistoricoPrecosPecas),
                ListaAnoModeloPreco = SetListAnoModeloPrecoCreate(input.ListaAnoModeloPreco),
                Pecas = new PecasEntity { ID_PECA = input.ID_PECA, DESCRICAO = input.DESC_PECA },
                MarcasPecas = new MarcasPecasEntity { ID_MARCA_PECAS = input.ID_MARCA_PECAS, DESCRICAO = input.DESC_MARCA_PECAS },
                MarcasVeiculos = new MarcasVeiculosEntity { ID_MARCA = input.ID_MARCA, DESCRICAO = input.DESC_MARCA },
                ModelosVeiculos = new ModelosVeiculosEntity { ID_MODELO = input.ID_MODELO, DESCRICAO = input.DESC_MODELO, ID_MARCA = input.ID_MARCA }
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
                HistoricoPrecosPecas = SetHistoricoPrecosPecasUpdate(input.HistoricoPrecosPecas),
                ListaAnoModeloPreco = SetListAnoModeloPrecoUpdate(input.ListaAnoModeloPreco),
                Pecas = new PecasEntity { ID_PECA = input.ID_PECA, DESCRICAO = input.DESC_PECA },
                MarcasVeiculos = new MarcasVeiculosEntity { ID_MARCA = input.ID_MARCA, DESCRICAO = input.DESC_MARCA },
                ModelosVeiculos = new ModelosVeiculosEntity { ID_MODELO = input.ID_MODELO, DESCRICAO = input.DESC_MODELO, ID_MARCA = input.ID_MARCA }
            };
        }

        private static List<HistoricosPrecoPecasEntity> SetHistoricoPrecosPecasUpdate(PrecoPecasUpdateInput.HistoricosPrecoPecasUpdateInput historicoPrecosPecas)
        {
            throw new NotImplementedException();
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
        private static List<HistoricosPrecoPecasEntity> SetHistoricoPrecosPecasUpdate(IEnumerable<PrecoPecasUpdateInput.HistoricosPrecoPecasUpdateInput> historicoPrecosPecas)
        {
            List<HistoricosPrecoPecasEntity> entites = new List<HistoricosPrecoPecasEntity>();

            foreach (var item in historicoPrecosPecas)
                entites.Add(new HistoricosPrecoPecasEntity
                {
                    ID_FORNECEDOR = item.ID_FORNECEDOR,
                    ID_HIST_PRECO_PECA = item.ID_HIST_PRECO_PECA,
                    STATUS = item.STATUS,
                    EstoquePecas = new EstoquePecasEntity
                    {
                        ID_ESTOQUE_PECAS = item.ID_HIST_PRECO_PECA,
                        QTDE_ESTOQUE = item.EstoquePecas.QTDE_ESTOQUE,
                        LOTE = item.EstoquePecas.LOTE,
                    },
                    Fornecedores = new FornecedoresEntity { ID_FORNECEDOR = item.ID_FORNECEDOR, DESCRICAO = item.DESC_FORNECEDOR }
                });

            return entites;
        }

        private static List<HistoricosPrecoPecasEntity> SetHistoricoPrecosPecasCreate(IEnumerable<PrecoPecasCreateInput.HistoricosPrecoPecasCreateInput> historicoPrecosPecas)
        {
            List<HistoricosPrecoPecasEntity> entites = new List<HistoricosPrecoPecasEntity>();

            foreach (var item in historicoPrecosPecas)
                entites.Add(new HistoricosPrecoPecasEntity
                {
                    ID_FORNECEDOR = item.ID_FORNECEDOR,
                    PRECO = item.PRECO,
                    CUSTO = item.CUSTO,
                    DATA_PRECO = DateTime.Now,
                    EstoquePecas = new EstoquePecasEntity
                    {
                        QTDE_ESTOQUE = item.EstoquePecas.QTDE_ESTOQUE,
                        LOTE = item.EstoquePecas.LOTE,
                    },
                    Fornecedores = new FornecedoresEntity { ID_FORNECEDOR = item.ID_FORNECEDOR, DESCRICAO = item.DESC_FORNECEDOR }
                });

            return entites;
        }
    }
}
