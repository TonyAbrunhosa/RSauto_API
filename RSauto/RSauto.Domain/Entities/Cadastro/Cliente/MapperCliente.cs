using RSauto.Domain.Entities.Cadastro.Cliente.Input;

namespace RSauto.Domain.Entities.Cadastro.Cliente
{
    public static class MapperCliente
    {
        public static ClienteEntity ToMapper(this ClienteInput input, int id = 0)
        {
            return new ClienteEntity
            {
                ID_CLIENTE = id,
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
