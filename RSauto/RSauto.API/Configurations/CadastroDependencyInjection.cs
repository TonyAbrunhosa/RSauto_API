using Microsoft.Extensions.DependencyInjection;
using RSauto.Application.Services.Cadastros;
using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities.Cadastro.AnoModeloVeiculo;
using RSauto.Domain.Entities.Cadastro.MarcasPecas;
using RSauto.Domain.Entities.Cadastro.MarcasVeiculos;
using RSauto.Domain.Entities.Cadastro.ModelosVeiculos;
using RSauto.Infrastructure.Repositories.Cadastros;
using RSauto.Infrastructure.Repositories.Registers;

namespace RSauto.API.Configurations
{
    public static class CadastroDependencyInjection
    {
        public static void AddCadastroDependencyInjection(this IServiceCollection services)
        {
            //SERVICES
            services.AddTransient<IAnoModeloVeiculoService, AnoModeloVeiculoService>();
            services.AddTransient<IAnoModeloVeiculoQueryService, AnoModeloVeiculoQueryService>();
            services.AddTransient<IMarcasPecasService, MarcasPecasService>();
            services.AddTransient<IMarcasPecasQueryService, MarcasPecasQueryService>();
            services.AddTransient<IMarcasVeiculosService, MarcasVeiculosService>();
            services.AddTransient<IMarcasVeiculosQueryService, MarcasVeiculosQueryService>();
            services.AddTransient<IModelosVeiculosService, ModelosVeiculosService>();
            services.AddTransient<IModelosVeiculosQueryService, ModelosVeiculosQueryService>();

            //REPOSITORY
            services.AddTransient<IBaseCrudRepository, BaseCrudRepository>();
            services.AddTransient<IAnoModeloVeiculoQueryRepository, AnoModeloVeiculoQueryRepository>();
            services.AddTransient<IMarcasPecasQueryRepository, MarcasPecasQueryRepository>();
            services.AddTransient<IMarcasVeiculosQueryRepository, MarcasVeiculosQueryRepository>();
            services.AddTransient<IModelosVeiculosQueryRepository, ModelosVeiculosQueryRepository>();

            //VALIDATE
            services.AddTransient<AnoModeloVeiculoNewValidate, AnoModeloVeiculoNewValidate>();
            services.AddTransient<AnoModeloVeiculoEditValidate, AnoModeloVeiculoEditValidate>();
            services.AddTransient<MarcasVeiculosNewValidate, MarcasVeiculosNewValidate>();
            services.AddTransient<MarcasVeiculosEditValidate, MarcasVeiculosEditValidate>();
            services.AddTransient<ModelosVeiculosValidate, ModelosVeiculosValidate>();
            services.AddTransient<MarcasPecasNewValidate, MarcasPecasNewValidate>();
            services.AddTransient<MarcasPecasEditValidate, MarcasPecasEditValidate>();
        }
    }
}
