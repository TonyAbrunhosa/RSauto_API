using Microsoft.Extensions.DependencyInjection;
using RSauto.Application.Services.Cadastros;
using RSauto.Domain.Contracts.Repositories.Cadastros;
using RSauto.Domain.Contracts.Services.Cadastros;
using RSauto.Domain.Entities.Cadastro.AnoModeloVeiculo;
using RSauto.Domain.Entities.Cadastro.MarcasPecas;
using RSauto.Domain.Entities.Cadastro.MarcasVeiculos;
using RSauto.Domain.Entities.Cadastro.ModelosVeiculos;
using RSauto.Infrastructure.Repositories.Cadastros;

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
            services.AddTransient<IAnoModeloVeiculoRepository, AnoModeloVeiculoRepository>();
            services.AddTransient<IAnoModeloVeiculoQueryRepository, AnoModeloVeiculoQueryRepository>();
            services.AddTransient<IMarcasPecasRepository, MarcasPecasRepository>();
            services.AddTransient<IMarcasPecasQueryRepository, MarcasPecasQueryRepository>();
            services.AddTransient<IMarcasVeiculosRepository, MarcasVeiculosRepository>();
            services.AddTransient<IMarcasVeiculosQueryRepository, MarcasVeiculosQueryRepository>();
            services.AddTransient<IModelosVeiculosRepository, ModelosVeiculosRepository>();
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
