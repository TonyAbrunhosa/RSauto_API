using RSauto.Domain.Entities.Cadastro.Fornecedor.Input;

namespace RSauto.Domain.Entities.Cadastro.Fornecedor
{
    public static class MapperFornecedor
    {
        public static FornecedorEntity ToMapper(this FornecedorInput input, int id = 0)
        {
            return new FornecedorEntity
            {
                ID_FORNECEDOR = id,
                NOME = input.Nome,
                RAZAO_SOCIAL = input.RazaoSocial,
                CPF_CNPJ = input.documento,
                TELEFONE = input.Telefone,
                CELULAR = input.Celular,
                EMAIL = input.Email,
                CEP = input.Cep,
                LOGRADOURO = input.Logradouro,
                NUMERO = input.Numero,
                COMPLEMENTO = input.Complemento,
                CIDADE = input.Cidade,
                ESTADO = input.Estado
            };
        }
    }
}
