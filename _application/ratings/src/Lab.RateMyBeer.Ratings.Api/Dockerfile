#syntax=docker/dockerfile:1.4

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /Application
COPY ["Lab.RateMyBeer.Ratings.Api/Lab.RateMyBeer.Ratings.Api.csproj", "Ratings/src/Lab.RateMyBeer.Ratings.Api/"]
COPY ["Lab.RateMyBeer.Ratings.Contracts/Lab.RateMyBeer.Ratings.Contracts.csproj", "Ratings/src/Lab.RateMyBeer.Ratings.Contracts/"]
COPY ["Lab.RateMyBeer.Ratings.Data/Lab.RateMyBeer.Ratings.Data.csproj", "Ratings/src/Lab.RateMyBeer.Ratings.Data/"]
COPY --from=framework ["Lab.RateMyBeer.Framework/Lab.RateMyBeer.Framework.csproj", "Framework/src/Lab.RateMyBeer.Framework/"]
COPY --from=frontend ["Lab.RateMyBeer.Frontend.Contracts/Lab.RateMyBeer.Frontend.Contracts.csproj", "Frontend/src/Lab.RateMyBeer.Frontend.Contracts/"]
RUN dotnet restore "Ratings/src/Lab.RateMyBeer.Ratings.Api/Lab.RateMyBeer.Ratings.Api.csproj"

COPY [".", "Ratings/src"]
COPY --from=framework ["Lab.RateMyBeer.Framework/", "Framework/src/Lab.RateMyBeer.Framework/"]
COPY --from=frontend ["Lab.RateMyBeer.Frontend.Contracts/", "Frontend/src/Lab.RateMyBeer.Frontend.Contracts/"]

WORKDIR /Application/Ratings/src/Lab.RateMyBeer.Ratings.Api
RUN dotnet publish "Lab.RateMyBeer.Ratings.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Lab.RateMyBeer.Ratings.Api.dll"]