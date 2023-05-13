#syntax=docker/dockerfile:1.4

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /Application
COPY ["Lab.RateMyBeer.Comments/Lab.RateMyBeer.Comments.csproj", "Comments/src/Lab.RateMyBeer.Comments/"]
COPY ["Lab.RateMyBeer.Comments.Contracts/Lab.RateMyBeer.Comments.Contracts.csproj", "Comments/src/Lab.RateMyBeer.Comments.Contracts/"]
COPY ["Lab.RateMyBeer.Comments.Data/Lab.RateMyBeer.Comments.Data.csproj", "Comments/src/Lab.RateMyBeer.Comments.Data/"]
COPY --from=framework ["Lab.RateMyBeer.Framework/Lab.RateMyBeer.Framework.csproj", "Framework/src/Lab.RateMyBeer.Framework/"]
COPY --from=frontend ["Lab.RateMyBeer.Frontend.Contracts/Lab.RateMyBeer.Frontend.Contracts.csproj", "Frontend/src/Lab.RateMyBeer.Frontend.Contracts/"]
RUN dotnet restore "Comments/src/Lab.RateMyBeer.Comments/Lab.RateMyBeer.Comments.csproj"

COPY [".", "Comments/src"]
COPY --from=framework ["Lab.RateMyBeer.Framework/", "Framework/src/Lab.RateMyBeer.Framework/"]
COPY --from=frontend ["Lab.RateMyBeer.Frontend.Contracts/", "Frontend/src/Lab.RateMyBeer.Frontend.Contracts/"]

WORKDIR /Application/Comments/src/Lab.RateMyBeer.Comments
RUN dotnet publish "Lab.RateMyBeer.Comments.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Lab.RateMyBeer.Comments.dll"]