using System;
using System.Net.Http;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.ApiClient;
using Lab.RateMyBeer.Comments.Contracts.Comments.ApiClient;
using Lab.RateMyBeer.Frontend.Api.Infrastructure;
using Lab.RateMyBeer.Ratings.Contracts.StarRatings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddHttpClientWithBaseUrl<ICheckinsRestApi>(checkinsApiBaseUrl);
          
            var ratingsApiBaseUrl = Configuration["Dependencies:APIs:RatingsApiBaseUrl"];
            services.AddHttpClientWithBaseUrl<IRatingsRestApi>(ratingsApiBaseUrl);
            
            var commentsApiBaseUrl = Configuration["Dependencies:APIs:CommentsApiBaseUrl"];
            services.AddHttpClientWithBaseUrl<ICommentsRestApi>(commentsApiBaseUrl);
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
