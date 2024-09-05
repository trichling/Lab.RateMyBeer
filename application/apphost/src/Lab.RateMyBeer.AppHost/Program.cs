var builder = DistributedApplication.CreateBuilder(args);

var checkinsApi = builder.AddProject("checkinsApi", @"..\..\..\checkins\src\Lab.RateMyBeer.Checkins.Api\Lab.RateMyBeer.Checkins.Api.csproj");
var commentsApi = builder.AddProject("commentsApi", @"..\..\..\comments\src\Lab.RateMyBeer.Comments.Api\Lab.RateMyBeer.Comments.Api.csproj");
var ratingsApi = builder.AddProject("ratingsApi", @"..\..\..\ratings\src\Lab.RateMyBeer.Ratings.Api\Lab.RateMyBeer.Ratings.Api.csproj");

var frontedApi = builder.AddProject("frontendApi", @"..\..\..\frontend\src\Lab.RateMyBeer.Frontend.Api\Lab.RateMyBeer.Frontend.Api.csproj")
// .WithReference(checkinsApi)
// .WithReference(commentsApi)
// .WithReference(ratingsApi);
;

builder.Build().Run();
