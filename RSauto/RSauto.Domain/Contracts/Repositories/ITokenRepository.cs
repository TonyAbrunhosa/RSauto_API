using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Token.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories
{
    public interface ITokenRepository
    {
        Task<LoginsEntity> BuscarUsuario(DadosTokenInput input);
    }
}
