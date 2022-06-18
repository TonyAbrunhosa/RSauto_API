using RSauto.Domain.Entities.Cadastro.ListaPrecoPecas.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Cadastros
{
    public interface IListaPrecoPecasQueryRepository
    {
        Task<IEnumerable<ListarListaPrecoPecasOutput>> Listar();
        Task<ListaPrecoPecasOutput> Buscar(int idPrecoPeca);
        Task<bool> PrecoOuCustoFoiAlterado(int idPrecoPeca, decimal custo, decimal preco);
        Task<IEnumerable<PesquisaListaPrecoPecasOutput>> PesquisaListaPrecoPecas(int IdModelo, int IdAnoModeloVeiculo);
    }
}
