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
using FluentValidation.AspNetCore;
using FluentValidation;
using RSauto.Domain.Entities.Cadastro.Cilindrada;
using RSauto.Application.Services.Registers;

namespace RSauto.API.Configurations
{
    public static class CadastroDependencyInjection
    {
        public static void AddCadastroDependencyInjection(this IServiceCollection services)
        {
            //SERVICES
            services.AddTransient<IAnoModeloVeiculoService, AnoModeloVeiculoService>();            
            services.AddTransient<IMarcasPecasService, MarcasPecasService>();            
            services.AddTransient<IMarcasVeiculosService, MarcasVeiculosService>();            
            services.AddTransient<IModelosVeiculosService, ModelosVeiculosService>();
            services.AddTransient<ICilindradaVeiculosService, CilindradaVeiculosService>();

            //REPOSITORY
            services.AddTransient<IBaseCrudRepository, BaseCrudRepository>();
            services.AddTransient<IAnoModeloVeiculoRepository, AnoModeloVeiculoRepository>();
            services.AddTransient<IMarcasPecasRepository, MarcasPecasRepository>();
            services.AddTransient<IMarcasVeiculosRepository, MarcasVeiculosRepository>();
            services.AddTransient<IModelosVeiculosRepository, ModelosVeiculosRepository>();
            services.AddTransient<ICilindradaVeiculosRepository, CilindradaVeiculosRepository>();

            //VALIDATE
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<AnoModeloVeiculoNewValidate>();
            services.AddValidatorsFromAssemblyContaining<AnoModeloVeiculoEditValidate>();
            services.AddValidatorsFromAssemblyContaining<MarcasVeiculosNewValidate>();
            services.AddValidatorsFromAssemblyContaining<MarcasVeiculosEditValidate>();
            services.AddValidatorsFromAssemblyContaining<ModelosVeiculosValidate>();
            services.AddValidatorsFromAssemblyContaining<MarcasPecasNewValidate>();
            services.AddValidatorsFromAssemblyContaining<MarcasPecasEditValidate>();
            services.AddValidatorsFromAssemblyContaining<CilindradaEditValidate>();
            services.AddValidatorsFromAssemblyContaining<CilindradaNewValidate>();

        }
    }
}
