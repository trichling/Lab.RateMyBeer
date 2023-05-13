#syntax=docker/dockerfile:1.4

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lab.RateMyBeer.Frontend/Lab.RateMyBeer.Frontend.csproj", "Lab.RateMyBeer.Frontend/"]
COPY ["Lab.RateMyBeer.Frontend.Contracts/Lab.RateMyBeer.Frontend.Contracts.csproj", "Lab.RateMyBeer.Frontend.Contracts/"]
RUN dotnet restore "Lab.RateMyBeer.Frontend/Lab.RateMyBeer.Frontend.csproj"
COPY . .
WORKDIR "/src/Lab.RateMyBeer.Frontend"

RUN dotnet publish "Lab.RateMyBeer.Frontend.csproj" -c Release -o /app/publish

FROM nginx:stable-alpine AS final
COPY --from=build /app/publish /usr/share/nginx/html
COPY Lab.RateMyBeer.Frontend/entrypoint.sh /docker-entrypoint.d/40-custom-settings.sh
COPY Lab.RateMyBeer.Frontend/nginx.conf.template /etc/nginx/nginx.conf.template
CMD ["nginx", "-g", "daemon off;"]