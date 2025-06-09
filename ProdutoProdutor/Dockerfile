FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Core ./Core
COPY Infrastructure ./Infrastructure
COPY ProdutoProdutor ./ProdutoProdutor

WORKDIR /src/ProdutoProdutor

RUN dotnet restore ProdutoProdutor.csproj
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "ProdutoProdutor.dll"]