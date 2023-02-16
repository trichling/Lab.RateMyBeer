#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lab.RateMyBeer.Ratings/Lab.RateMyBeer.Ratings.csproj", "Lab.RateMyBeer.Ratings/"]
COPY ["Lab.RateMyBeer.Ratings.Contracts/Lab.RateMyBeer.Ratings.Contracts.csproj", "Lab.RateMyBeer.Ratings.Contracts/"]
COPY ["Lab.RateMyBeer.Ratings.Data/Lab.RateMyBeer.Ratings.Data.csproj", "Lab.RateMyBeer.Ratings.Data/"]
RUN dotnet restore "Lab.RateMyBeer.Ratings/Lab.RateMyBeer.Ratings.csproj"
COPY . .
WORKDIR "/src/Lab.RateMyBeer.Ratings"
RUN dotnet build "Lab.RateMyBeer.Ratings.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab.RateMyBeer.Ratings.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab.RateMyBeer.Ratings.dll"]