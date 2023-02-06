using Microsoft.Extensions.DependencyInjection;
using RSauto.Application.Services;
using RSauto.Application.Services.Cadastros;
using RSauto.Domain.Contracts.Repositories;
using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Contracts.Services;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Infrastructure.Repositories;
using RSauto.Infrastructure.Repositories.Cadastros;
using RSauto.Shared.Communication;
using RSauto.Shared.Utilities;

namespace RSauto.API.Configurations
{
    public static class DependencyInjection
    {
        public static void ResolveDependencyInjection(this IServiceCollection services)
        {            
            services.AddScoped<AppSettings, AppSettings>();
            services.AddScoped<SqlCommunication, SqlCommunication>();            

            //SERVICES
            services.AddTransient<ITokenService, TokenService>();

            //REPOSITORY
            services.AddTransient<ITokenRepository, TokenRepository>();

            services.AddCadastroDependencyInjection();
        }
    }
}
