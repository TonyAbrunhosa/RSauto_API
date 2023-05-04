using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using RSauto.API.Configurations;
using RSauto.API.Middlewares;

namespace RSauto.API
{
    public class Startup
    {        
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.ResolveDependencyInjection();
            services.ResolveCors();            
            services.AddAuthenticationBearer();
            services.AddSwagger("RSauto.Api", "v1");
            services.AddCompression();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddFluentValidationAutoValidation();
        }
                
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "RSauto.Api v1"); });
            app.UseRouting();
            app.UseCors(Cors.origins);
            app.UseHttpsRedirection();            
            app.UseResponseCompression();
            app.UseAuthorization();
            app.UseBaseMiddleware();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
