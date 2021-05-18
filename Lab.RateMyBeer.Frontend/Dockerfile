#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Lab.RateMyBeer.Frontend/Lab.RateMyBeer.Frontend.csproj", "Lab.RateMyBeer.Frontend/"]
COPY ["Lab.RateMyBeer.Frontend.Contracts/Lab.RateMyBeer.Frontend.Contracts.csproj", "Lab.RateMyBeer.Frontend.Contracts/"]
RUN dotnet restore "Lab.RateMyBeer.Frontend/Lab.RateMyBeer.Frontend.csproj"
COPY . .
WORKDIR "/src/Lab.RateMyBeer.Frontend"
RUN dotnet build "Lab.RateMyBeer.Frontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab.RateMyBeer.Frontend.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish .
COPY ["Lab.RateMyBeer.Frontend/nginx.conf", "/etc/nginx/nginx.conf"]
