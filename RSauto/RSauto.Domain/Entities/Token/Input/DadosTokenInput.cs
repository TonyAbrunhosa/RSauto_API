using RSauto.Shared.Utilities;
using System.Text.Json.Serialization;

namespace RSauto.Domain.Entities.Token.Input
{
    public class DadosTokenInput
    {
        public DadosTokenInput(string login, string senha)
        {
            Login = login;
            Senha = senha;
            SenhaHash = AdmHash.Encrypt(Senha);
        }

        public string Login { get; set; }
        public string Senha { get; set; }
        [JsonIgnore]
        public string SenhaHash { get; set; }
    }
}
