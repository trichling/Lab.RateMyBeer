#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Lab.RateMyBeer.Checkins/Lab.RateMyBeer.Checkins.csproj", "Lab.RateMyBeer.Checkins/"]
COPY ["Lab.RateMyBeer.Checkins.Contracts/Lab.RateMyBeer.Checkins.Contracts.csproj", "Lab.RateMyBeer.Checkins.Contracts/"]
RUN dotnet restore "Lab.RateMyBeer.Checkins/Lab.RateMyBeer.Checkins.csproj"
COPY . .
WORKDIR "/src/Lab.RateMyBeer.Checkins"
RUN dotnet build "Lab.RateMyBeer.Checkins.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab.RateMyBeer.Checkins.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab.RateMyBeer.Checkins.dll"]