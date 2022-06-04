using ApiDesafio.Config.Behaviors;
using Domain.Interfaces;
using Domain.Interfaces.Generics;
using Infra.Repository.Generics;
using Infra.Repository.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace ApiDesafio
{
    public class Startup
    {
        private readonly string swaggerBasePath = "other-lobs";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning();
            services.AddHealthChecks();

            services.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
            {
                apiBehaviorOptions.InvalidModelStateResponseFactory = new InvalidModelState().Configure;
            });

            services.AddSingleton<IPessoa, RepositoryPessoa>();
            services.AddSingleton<ICidade, RepositoryCidade>();
            services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGenerics<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {            
            app.UseSwagger(c =>
            {
                c.RouteTemplate = swaggerBasePath + "/swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {

                    options.SwaggerEndpoint($"/{swaggerBasePath}/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                    options.RoutePrefix = $"{swaggerBasePath}/swagger";
                }

            });
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/{swaggerBasePath}/health-check", new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        context.Response.ContentType = "application/json";
                        var response = new
                        {
                            status = "OK",
                            timestamp = DateTime.Now.ToString("yyyy-MM-dd'T'hh:mm:ss'Z'")
                        };
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    }

                });
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
