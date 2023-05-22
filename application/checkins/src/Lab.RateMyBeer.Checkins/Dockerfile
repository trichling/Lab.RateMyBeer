#syntax=docker/dockerfile:1.4

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /Application
COPY ["Lab.RateMyBeer.Checkins/Lab.RateMyBeer.Checkins.csproj", "Checkins/src/Lab.RateMyBeer.Checkins/"]
COPY ["Lab.RateMyBeer.Checkins.Contracts/Lab.RateMyBeer.Checkins.Contracts.csproj", "Checkins/src/Lab.RateMyBeer.Checkins.Contracts/"]
COPY ["Lab.RateMyBeer.Checkins.Data/Lab.RateMyBeer.Checkins.Data.csproj", "Checkins/src/Lab.RateMyBeer.Checkins.Data/"]
COPY --from=framework ["Lab.RateMyBeer.Framework/Lab.RateMyBeer.Framework.csproj", "Framework/src/Lab.RateMyBeer.Framework/"]
COPY --from=frontend ["Lab.RateMyBeer.Frontend.Contracts/Lab.RateMyBeer.Frontend.Contracts.csproj", "Frontend/src/Lab.RateMyBeer.Frontend.Contracts/"]
RUN dotnet restore "Checkins/src/Lab.RateMyBeer.Checkins/Lab.RateMyBeer.Checkins.csproj"

COPY [".", "Checkins/src"]
COPY --from=framework ["Lab.RateMyBeer.Framework/", "Framework/src/Lab.RateMyBeer.Framework/"]
COPY --from=frontend ["Lab.RateMyBeer.Frontend.Contracts/", "Frontend/src/Lab.RateMyBeer.Frontend.Contracts/"]

WORKDIR /Application/Checkins/src/Lab.RateMyBeer.Checkins
RUN dotnet publish "Lab.RateMyBeer.Checkins.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Lab.RateMyBeer.Checkins.dll"]