FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todas as pastas necessárias
COPY Core ./Core
COPY Infrastructure ./Infrastructure
COPY ProdutoConsumidor ./ProdutoConsumidor

WORKDIR /src/ProdutoConsumidor

RUN dotnet restore ProdutoConsumidor.csproj
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ProdutoConsumidor.dll"]