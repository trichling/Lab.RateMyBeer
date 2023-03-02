FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
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
COPY ["Lab.RateMyBeer.Frontend/nginx.conf.template", "/etc/nginx/nginx.conf.template"]
COPY ["Lab.RateMyBeer.Frontend/entrypoint.sh", "/"]
ENTRYPOINT ["/entrypoint.sh"]
CMD ["nginx", "-g", "daemon off;"]