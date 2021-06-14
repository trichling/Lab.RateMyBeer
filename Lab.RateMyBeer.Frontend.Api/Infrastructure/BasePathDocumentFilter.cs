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
                Url = "http://ratemybeer.westeurope.cloudapp.azure.com/api/"
            });
        }
    }
}