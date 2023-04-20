#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lab.RateMyBeer.Comments.Api/Lab.RateMyBeer.Comments.Api.csproj", "Lab.RateMyBeer.Comments.Api/"]
COPY ["Lab.RateMyBeer.Comments.Contracts/Lab.RateMyBeer.Comments.Contracts.csproj", "Lab.RateMyBeer.Comments.Contracts/"]
COPY ["Lab.RateMyBeer.Comments.Data/Lab.RateMyBeer.Comments.Data.csproj", "Lab.RateMyBeer.Comments.Data/"]
RUN dotnet restore "Lab.RateMyBeer.Comments.Api/Lab.RateMyBeer.Comments.Api.csproj"
COPY . .
WORKDIR "/src/Lab.RateMyBeer.Comments.Api"
RUN dotnet build "Lab.RateMyBeer.Comments.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab.RateMyBeer.Comments.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab.RateMyBeer.Comments.Api.dll"]