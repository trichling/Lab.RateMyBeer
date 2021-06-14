using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.ApiClient;
using Lab.RateMyBeer.Frontend.Api.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RestEase;

namespace Lab.RateMyBeer.Frontend.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lab.RateMyBeer.Api", Version = "v1" });
                c.DocumentFilter<BasePathDocumentFilter>();
            });

            services.AddCors(builder => builder.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));


            
            var checkinsApiBaseUrl = Configuration["Dependencies:APIs:CheckinsApiBaseUrl"];
            services.AddHttpClient(nameof(ICheckinsRestApi))
                .ConfigurePrimaryHttpMessageHandler(p => 
                {
                    var handler = new HttpClientHandler();
                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    handler.ServerCertificateCustomValidationCallback = 
                        (httpRequestMessage, cert, cetChain, policyErrors) =>
                        {
                            return true;
                        };

                    return handler;
                })
            .ConfigureHttpClient((services, client) => 
                {
                    client.BaseAddress = new Uri(checkinsApiBaseUrl);
                });

            services.AddTransient<ICheckinsRestApi>(p => 
            {
                var client = p.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(ICheckinsRestApi));
                return RestClient.For<ICheckinsRestApi>(client);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                // use relative URI, not /swagger/v1/swagger.json otherwise urls wont be resolved correctly!!
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("v1/swagger.json", "Lab.RateMyBeer.Api v1");
                });
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
