using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lab.RateMyBeer.Frontend.Api.Infrastructure
{
    public class BasePathDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Servers.Add(new OpenApiServer() {
                Url = "/api"
            });

            swaggerDoc.Servers.Add(new OpenApiServer() {
                Url = "/",
            });
        }
    }
}