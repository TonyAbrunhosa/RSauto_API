using Dapper.Contrib.Extensions;

namespace RSauto.Domain.Entities
{
    [Table("CLIENTES")]
    public class ClienteEntity
    {
        [Key]
        public int ID_CLIENTE { get; set; }
        public string NOME { get; set; }
        public string RAZAO_SOCIAL { get; set; }
        public string CPF_CNPJ { get; set; }
        public string TELEFONE { get; set; }
        public string CELULAR { get; set; }
        public string EMAIL { get; set; }
        public string CEP { get; set; }
        public string LOGRADOURO { get; set; }
        public string NUMERO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string CIDADE { get; set; }
        public string ESTADO { get; set; }        
    }
}
