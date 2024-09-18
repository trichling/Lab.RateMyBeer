var builder = DistributedApplication.CreateBuilder(args);

var serviceBus = builder.AddParameter("serviceBus", secret: true);

var checkinsApi = builder.AddProject("checkinsApi", @"..\..\..\checkins\src\Lab.RateMyBeer.Checkins.Api\Lab.RateMyBeer.Checkins.Api.csproj")
    .WithEnvironment("Dependencies__NServiceBus__TransportConnectionString", serviceBus);

var checkins = builder.AddProject("checkins", @"..\..\..\checkins\src\Lab.RateMyBeer.Checkins\Lab.RateMyBeer.Checkins.csproj");

var commentsApi = builder.AddProject("commentsApi", @"..\..\..\comments\src\Lab.RateMyBeer.Comments.Api\Lab.RateMyBeer.Comments.Api.csproj");
var comments = builder.AddProject("comments", @"..\..\..\comments\src\Lab.RateMyBeer.Comments\Lab.RateMyBeer.Comments.csproj");

var ratingsApi = builder.AddProject("ratingsApi", @"..\..\..\ratings\src\Lab.RateMyBeer.Ratings.Api\Lab.RateMyBeer.Ratings.Api.csproj");
var ratings = builder.AddProject("ratings", @"..\..\..\ratings\src\Lab.RateMyBeer.Ratings\Lab.RateMyBeer.Ratings.csproj");

var frontedApi = builder.AddProject("frontendApi", @"..\..\..\frontend\src\Lab.RateMyBeer.Frontend.Api\Lab.RateMyBeer.Frontend.Api.csproj")
.WithReference(checkinsApi)
.WithReference(commentsApi)
.WithReference(ratingsApi);
;

builder.Build().Run();
