using Lab.RateMyBeer.Comments.Data.Comments;
using Microsoft.EntityFrameworkCore;

namespace Lab.RateMyBeer.Comments.Api.Comments;

public static class CommentsModule
{

    public static IServiceCollection RegisterCommentsModule(this IServiceCollection services, IConfiguration configuration)
    {
        var commentsDbConnectionString = configuration.GetConnectionString("CommentsDbConnectionString");
        commentsDbConnectionString = string.Format(commentsDbConnectionString, "CommentsDb");

        services.AddDbContext<CommentsContext>(options =>
        {
            options.UseSqlServer(commentsDbConnectionString);
        });
        
        return services;
    }
    
    public static IEndpointRouteBuilder MapCommentsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/comments", GetComments.Handle);
        
        return endpoints;
    }
}