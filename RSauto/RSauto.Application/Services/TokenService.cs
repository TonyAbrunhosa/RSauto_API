using Microsoft.IdentityModel.Tokens;
using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Repositories;
using RSauto.Domain.Contracts.Services;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Command;
using RSauto.Domain.Entities.Token.Input;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RSauto.Application.Services
{
    public class TokenService : ITokenService
    {        
        
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<ICommandResult> ObterToken(DadosTokenInput input)
        {
            var dados = new LoginsEntity { ID_USUARIO = 1, LOGIN_USUARIO = "teste", NOME_USUARIO = "Teste", SENHA_USUARIO = "1234" }; //await _tokenRepository.BuscarUsuario(input);

            if ((dados?.ID_USUARIO ?? 0) == 0)
                return new CommandResult(false, "Usuario não encontrado.");

            return new CommandResult(true, "Usuario encontrado com sucesso", GeraToken(dados));
        }
        private object GeraToken(LoginsEntity entity)
        {
            DateTime DataExpiracao = DateTime.Now.AddHours(6);
            var claims = new[] { new Claim(ClaimTypes.Name, entity.NOME_USUARIO), new Claim(ClaimTypes.NameIdentifier, entity.LOGIN_USUARIO) };            

            var creds = new SigningCredentials(Loadkey(), SecurityAlgorithms.HmacSha256Signature);

            var jwtoken = new JwtSecurityToken(
             issuer: "RSauto",
             audience: "RSauto",
             claims: claims,
             expires: DataExpiracao,
             signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtoken);

            return new
            {
                Token = token,
                DataCriacao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DataExp = DataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                NomeUsuario = entity.NOME_USUARIO,
                LoginUsuario = entity.LOGIN_USUARIO
            };
        }
        private static byte[] GenerateKey(int bytes)
        {
            RandomNumberGenerator Rng = RandomNumberGenerator.Create();
            var data = new byte[bytes];
            Rng.GetBytes(data);
            return data;
        }

        private SecurityKey Loadkey()
        {
            string MyJwkLocation = Path.Combine(Environment.CurrentDirectory, "mysupersecretkey.json");

            if (File.Exists(MyJwkLocation))
                return JsonSerializer.Deserialize<JsonWebKey>(File.ReadAllText(MyJwkLocation));

            var newKey = CreateJWK();
            File.WriteAllText(MyJwkLocation, JsonSerializer.Serialize(newKey));
            return newKey;
        }

        private JsonWebKey CreateJWK()
        {
            var symetricKey = new HMACSHA256(GenerateKey(64));
            var jwk = JsonWebKeyConverter.ConvertFromSymmetricSecurityKey(new SymmetricSecurityKey(symetricKey.Key));
            jwk.KeyId = Base64UrlEncoder.Encode(GenerateKey(16));
            return jwk;
        }
    }
}
