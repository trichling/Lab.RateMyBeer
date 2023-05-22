#syntax=docker/dockerfile:1.4

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /Application
COPY ["Lab.RateMyBeer.Frontend.Api/Lab.RateMyBeer.Frontend.Api.csproj", "Frontend/src/Lab.RateMyBeer.Frontend.Api/"]
COPY ["Lab.RateMyBeer.Frontend.Contracts/Lab.RateMyBeer.Frontend.Contracts.csproj", "Frontend/src/Lab.RateMyBeer.Frontend.Contracts/"]
COPY --from=checkins ["Lab.RateMyBeer.Checkins.Contracts/Lab.RateMyBeer.Checkins.Contracts.csproj", "Checkins/src/Lab.RateMyBeer.Checkins.Contracts/"]
COPY --from=comments ["Lab.RateMyBeer.Comments.Contracts/Lab.RateMyBeer.Comments.Contracts.csproj", "Comments/src/Lab.RateMyBeer.Comments.Contracts/"]
COPY --from=ratings ["Lab.RateMyBeer.Ratings.Contracts/Lab.RateMyBeer.Ratings.Contracts.csproj", "Ratings/src/Lab.RateMyBeer.Ratings.Contracts/"]
COPY --from=framework ["Lab.RateMyBeer.Framework/Lab.RateMyBeer.Framework.csproj", "Framework/src/Lab.RateMyBeer.Framework/"]

RUN dotnet restore "Frontend/src/Lab.RateMyBeer.Frontend.Api/Lab.RateMyBeer.Frontend.Api.csproj"

COPY [".", "Frontend/src"]
COPY --from=framework ["Lab.RateMyBeer.Framework/", "Framework/src/Lab.RateMyBeer.Framework/"]
COPY --from=checkins ["Lab.RateMyBeer.Checkins.Contracts/", "Checkins/src/Lab.RateMyBeer.Checkins.Contracts/"]
COPY --from=comments ["Lab.RateMyBeer.Comments.Contracts/", "Comments/src/Lab.RateMyBeer.Comments.Contracts/"]
COPY --from=ratings ["Lab.RateMyBeer.Ratings.Contracts/", "Ratings/src/Lab.RateMyBeer.Ratings.Contracts/"]

WORKDIR /Application/Frontend/src/Lab.RateMyBeer.Frontend.Api
RUN dotnet publish "Lab.RateMyBeer.Frontend.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Lab.RateMyBeer.Frontend.Api.dll"]
